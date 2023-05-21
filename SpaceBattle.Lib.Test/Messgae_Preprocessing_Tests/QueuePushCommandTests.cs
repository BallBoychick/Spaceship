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

        var queue = new Queue<ICommand>();
        var mock_strategy = new Mock<IStrategy>();
        mock_strategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(queue).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetQueueOfGameById", (object[] args) => mock_strategy.Object.RunStrategy(args)).Execute();

        var commandMock = new Mock<ICommand>();

        var gameQueuePushCommand = new GameQueuePushCommand(1, commandMock.Object);
        Assert.Equal(0, queue.Count);
        gameQueuePushCommand.Execute();

        mock_strategy.VerifyAll();
        Assert.Equal(1, queue.Count);

    }
    [Fact]

    public void GameQueuePushCommand_CanBeConstructed()
    {
        var commandMock = new Mock<ICommand>();

        var gameQueuePushCommand = new GameQueuePushCommand(1, commandMock.Object);

        Assert.NotNull(gameQueuePushCommand);
    }
}
