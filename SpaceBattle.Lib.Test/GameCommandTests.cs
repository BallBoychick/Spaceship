namespace SpaceBattle.Lib.Test;
using System;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;
using SpaceBattle.Lib;
using Xunit;

public class GameCommandTests
{
    IDictionary<int, object> exceptionTree = new Dictionary<int, object>();
    void RegisterDependencies()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

    }
    [Fact]
    public void InitializeGame()
    {
        RegisterDependencies();
        Queue<Lib.ICommand> queue = new Queue<Lib.ICommand>();
        var mockCommand = new Mock<Lib.ICommand>();
        mockCommand.Setup(command => command.Execute()).Callback(() => { }).Verifiable();
        queue.Enqueue(mockCommand.Object);
        var stopGameCommand = new Mock<Lib.ICommand>();
        queue.Enqueue(stopGameCommand.Object);
        var reciever = new Mock<IReciever>();
        reciever.Setup(reciever => reciever.Recieve()).Returns(() =>
        {
            if (queue.Count != 0)
            {
                return queue.Dequeue();
            }
            return new Mock<Lib.ICommand>().Object;
        });
        reciever.Setup(reciever => reciever.IsEmpty()).Returns(queue.Count == 0);
        double quant = 40;
        var mockStrategy = new Mock<IStrategy>();
        mockStrategy.Setup(s => s.RunStrategy()).Returns(quant);
        var gameScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Quant", (object [] par)=> mockStrategy.Object.RunStrategy(par)).Execute();
        var game = new GameCommand(reciever.Object, gameScope);
        game.Execute();
        mockCommand.Verify();

    }

    [Fact]
    public void QuantTimeIsUp()
    {
        RegisterDependencies();
        Queue<Lib.ICommand> queue = new Queue<Lib.ICommand>();
        var mockCommand = new Mock<Lib.ICommand>();
        mockCommand.Setup(command => command.Execute()).Callback(() =>
        {
            Task.Delay(41);
        }).Verifiable();
        var mockCommand2 = new Mock<Lib.ICommand>();
        mockCommand2.Setup(command => command.Execute()).Callback(() =>
        {

        }).Verifiable();
        queue.Enqueue(mockCommand.Object);
        queue.Enqueue(mockCommand2.Object);
        var reciever = new Mock<IReciever>();
        reciever.Setup(reciever => reciever.Recieve()).Returns(() =>
        {
            if (queue.Count != 0)
            {
                return queue.Dequeue();
            }
            return new Mock<Lib.ICommand>().Object;
        });
        reciever.Setup(reciever => reciever.IsEmpty()).Returns(queue.Count == 0);
        double quant = 40;
        var mockStrategy = new Mock<IStrategy>();
        mockStrategy.Setup(s => s.RunStrategy()).Returns(quant);
        var gameScope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Quant", (object [] par)=> mockStrategy.Object.RunStrategy(par)).Execute();
        var game = new GameCommand(reciever.Object, gameScope);
        game.Execute();
        mockCommand.Verify();
        mockCommand2.VerifyNoOtherCalls();

    }
}