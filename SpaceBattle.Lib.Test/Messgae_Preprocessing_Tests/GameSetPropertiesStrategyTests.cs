using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;

public class GameSetPropertiesStrategyTests
{
    [Fact]
    public void SuccessfulGameSetPropertiesStrategyExecute()
    {
        var strategy = new GameSetPropertiesStrategy();
        var obj = Mock.Of<IUObject>();
        var cmd = strategy.RunStrategy(obj, "key", 1);

        Assert.NotNull(cmd);
    }
}
