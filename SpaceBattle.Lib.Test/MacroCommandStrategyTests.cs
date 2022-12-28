using Moq;

namespace SpaceBattle.Lib.Test;

public class CreateMacroCommandStrategyTests
{
    [Fact]
    public void HaveCreatedMacroCommandRunStrategy()
    {
        var mockIEnumerableCommand = new Mock<IEnumerable<ICommand>>();
        var createstrategy = new CreateMacroCommandStrategy();

        Assert.NotNull(createstrategy.RunStrategy(mockIEnumerableCommand.Object));
    }
}