using Hwdtech.Ioc;
using Hwdtech;

using Moq;

namespace SpaceBattle.Lib.Test;

public class CreateMacroStrategyTests
{
    [Fact]
    public void SuccesOfCreateMacroCommandStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mock_cmd = new Mock<ICommand>();

        var mockIUobject = new Mock<IUObject>();

        var mockStrategyReturnsMacroCommand = new Mock<IStrategy>();

        mockStrategyReturnsMacroCommand.Setup(a => a.RunStrategy(It.IsAny<IEnumerable<ICommand>>())).Returns(mock_cmd.Object).Verifiable();

        var mockStrategyReturnsCommand = new Mock<IStrategy>();

        mockStrategyReturnsMacroCommand.Setup(a => a.RunStrategy(It.IsAny<IUObject>())).Returns(mock_cmd.Object).Verifiable();

        var mockIEnumerablestring = new Mock<IEnumerable<string>>();

        var mockStrategyIEnumerablestring = new Mock<IStrategy>();
        
        mockStrategyIEnumerablestring.Setup(a => a.RunStrategy()).Returns(mockIEnumerablestring.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SetupStringOperation.Name", (object[] args) => mockStrategyIEnumerablestring.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateMacroCommand", (object[] args) => mockStrategyReturnsMacroCommand.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Name", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();

        var createstrategy = new CreateMacroStrategy();

        createstrategy.RunStrategy("Name", mockIUobject.Object);

        mockStrategyIEnumerablestring.Verify();
        mockStrategyReturnsCommand.Verify();
    }
}
