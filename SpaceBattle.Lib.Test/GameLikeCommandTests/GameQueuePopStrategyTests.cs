using Hwdtech;
using Hwdtech.Ioc;
using Moq;
namespace SpaceBattle.Lib;
public class GameQueuePopStrategyTests
{
    [Fact]

    public void Correct_Inputer_Strategy()
    {
        var game_id = 1;
        var commd = Mock.Of<ICommand>();
        var queueMock = new Mock<Queue<ICommand>>();
        var strategy = new GameQueuePopStrategy();
        
        var cmd = strategy.RunStrategy(game_id, commd);
        Assert.NotNull(cmd);
    }
}
