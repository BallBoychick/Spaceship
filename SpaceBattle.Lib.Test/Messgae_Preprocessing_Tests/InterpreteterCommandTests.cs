using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
public class InterpreterCommandTests
{
    [Fact]
    public void TestExecute()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockCommand = new Mock<ICommand>();
        mockCommand.Setup(x => x.Execute());

        var message = new Mock<IMessage>();
        message.Setup(m => m.GameID).Throws<Exception>();

        var mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsCommand.Setup(x => x.RunStrategy(It.IsAny<IMessage>())).Returns(mockCommand.Object);

        var queue = new Queue<ICommand>();

        // var mock_strategy = new Mock<IStrategy>();
        // mock_strategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(queue).Verifiable();


        var queuepushCommand = new Mock<ICommand>();
        queuepushCommand.Setup(c => c.Execute()).Callback(() => { queue.Enqueue(mockCommand.Object); });

        var queuePushStrategy = new Mock<IStrategy>();
        queuePushStrategy.Setup(s => s.RunStrategy(It.IsAny<int>(), It.IsAny<ICommand>())).Returns(queuepushCommand.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.CreateCommand", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Push", (object[] args) => queuePushStrategy.Object.RunStrategy(args)).Execute();

        var interpreterCommand = new InterpreterCommand(message.Object);

        Assert.Throws<Exception>(() => interpreterCommand.Execute());
            


    }
    
    [Fact]
    public void InterpreterCommand_CanBeConstructed()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var message = new Mock<IMessage>();
        message.Setup(m => m.GameID).Returns(1);


        var commandMock = new Mock<ICommand>();
        commandMock.Setup(x => x.Execute());
        var queue = new Queue<ICommand>();

        var queuepushCommand = new Mock<ICommand>();
        queuepushCommand.Setup(c => c.Execute()).Callback(() => { queue.Enqueue(commandMock.Object); });

        var queuePushStrategy = new Mock<IStrategy>();
        queuePushStrategy.Setup(s => s.RunStrategy(It.IsAny<int>(), It.IsAny<ICommand>())).Returns(queuepushCommand.Object);

        var mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsCommand.Setup(x => x.RunStrategy(It.IsAny<IMessage>())).Returns(commandMock.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.CreateCommand", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Push", (object[] args) => queuePushStrategy.Object.RunStrategy(args)).Execute();

        var InterpreterCommand = new InterpreterCommand(message.Object);

        InterpreterCommand.Execute();

        queuePushStrategy.VerifyAll();
        Assert.Equal(1, queue.Count);
    }    
}
