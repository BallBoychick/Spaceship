using Moq;

namespace SpaceBattle.Lib.Test;

public class MacroCommandStrategyTests
{
    [Fact]
    public void SuccesMacroCommandTestsExecute()
    {
        var mockCommand = new Mock<ICommand>();
        mockCommand.Setup(a => a.execute()).Verifiable();
        var list = new List<ICommand>() { mockCommand.Object };
        var command = new MacroCommand(list);

        command.execute();
        mockCommand.Verify();
    }
}
