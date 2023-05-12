using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
// public class InterpreterCommandTests
// {
//     [Fact]
//         public void TestExecute()
//         {
//             new InitScopeBasedIoCImplementationCommand().Execute();
//             IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
//             // Arrange
//             // var message = new Mock<IMessage>();
//             // message.Setup(m => m.GameID).Returns(123);

//             // var createCommand = new Mock<ICommand>();
//             // var commandQueue = new Queue<ICommand>();
            
//             // var id = 123;
//             // // commandQueue.(cq => cq.Push(123, createCommand.Object));
//             // var queue = new Mock<Queue<ICommand>>();
//             // var command = new Mock<ICommand>();
//             // var sut = new GameQueuePushCommand(id, command);
//         }
        
// }


public class QueuePushCommandTests
{
    [Fact]
    public void QueuePushCommandPush()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var queueMock = new Mock<Queue<ICommand>>();
        var mock_strategy = new Mock<IStrategy>();
        mock_strategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(queueMock.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetQueueOfGameById", (object[] args) => mock_strategy.Object.RunStrategy(args)).Execute();

        IoC.Resolve<Queue<ICommand>>("GetQueueOfGameById", 1);
        var commandMock = new Mock<ICommand>();

        var gameQueuePushCommand = new GameQueuePushCommand(1, commandMock.Object);

        gameQueuePushCommand.Execute();

        queueMock.Verify(q => q.Enqueue(commandMock.Object));
    }
    [Fact]

    public void GameQueuePushCommand_CanBeConstructed()
    {
        // Arrange
        var commandMock = new Mock<ICommand>();

        // Act
        var gameQueuePushCommand = new GameQueuePushCommand(1, commandMock.Object);

        // Assert
        Assert.NotNull(gameQueuePushCommand);
    }
}
