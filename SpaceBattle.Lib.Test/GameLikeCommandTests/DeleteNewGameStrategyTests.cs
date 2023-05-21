using Hwdtech;
using Hwdtech.Ioc;
using Moq;
namespace SpaceBattle.Lib;
public class DeleteNewGameStrategyTests
{
    [Fact]

    public void Correct_Inputer_Strategy()
    {
        var game_id = 1;
        var strategy = new DeleteNewGameStrategy();
        
        var cmd = strategy.RunStrategy(game_id);

        Assert.NotNull(cmd);
    }
}
