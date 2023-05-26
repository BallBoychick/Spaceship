using Hwdtech;
using Hwdtech.Ioc;
using Moq;
namespace SpaceBattle.Lib;
public class GameQueuePopStrategyTests
{
    [Fact]

    public void RunStrategy_ShouldReturnDequeuedCommand_Success()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var queue = new Queue<ICommand>();
        var mock_strategy = new Mock<IStrategy>();
        mock_strategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(queue).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetQueueOfGameById", (object[] args) => mock_strategy.Object.RunStrategy(args)).Execute();
        var commandMock = new Mock<ICommand>();


        var gameQueuePushCommand = new GameQueuePushCommand(1, commandMock.Object);
        gameQueuePushCommand.Execute();
        var game_id = 1;
        var strategy2 = new GameQueuePopStrategy();
        var cmd2 = strategy2.RunStrategy(game_id, commandMock.Object);
        Assert.Empty(IoC.Resolve<Queue<ICommand>>("GetQueueOfGameById", 1));
    }
}
