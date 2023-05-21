namespace SpaceBattle.Lib;
using Hwdtech;
using Hwdtech.Ioc;
using Moq;
public class GameQueuePushStrategyTests
{
    [Fact]

    public void Correct_Inputer_Strategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var strategy = new GameQueuePushStrategy();
        var id = 1;
        var commd = Mock.Of<ICommand>();
        var queueMock = new Mock<Queue<ICommand>>();
        var cmd = strategy.RunStrategy(id, commd);

        Assert.NotNull(cmd);
    }
}
