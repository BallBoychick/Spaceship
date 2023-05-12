namespace SpaceBattle.Lib;
using Hwdtech;
using Moq;
public class GameQueuePushStrategyTests
{
    [Fact]

    public void Correct_Inputer_Strategy()
    {
        var strategy = new GameQueuePushStrategy();
        var id = 1;
        var commandMock = new Mock<ICommand>();
        var cmd = strategy.RunStrategy(id, commandMock);

        Assert.NotNull(cmd);
    }
}