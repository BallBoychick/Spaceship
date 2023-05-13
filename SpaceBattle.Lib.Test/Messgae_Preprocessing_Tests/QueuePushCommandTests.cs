using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

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

        mock_strategy.VerifyAll();
    }
    [Fact]

    public void GameQueuePushCommand_CanBeConstructed()
    {
        var commandMock = new Mock<ICommand>();

        var gameQueuePushCommand = new GameQueuePushCommand(1, commandMock.Object);

        Assert.NotNull(gameQueuePushCommand);
    }
}
