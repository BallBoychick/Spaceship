using Moq;

namespace SpaceBattle.Lib.Test;

public class MacroCommandStrategyTests
{
    [Fact]
    public void SuccesMacroCommandTestsExecute()
    {
        var mockCommand = new Mock<ICommand>();
        mockCommand.Setup(a => a.Execute()).Verifiable();
        var list = new List<ICommand>(){mockCommand.Object};
        var command = new MacroCommand(list);
        
        command.Execute();
        mockCommand.Verify();
    }
}
