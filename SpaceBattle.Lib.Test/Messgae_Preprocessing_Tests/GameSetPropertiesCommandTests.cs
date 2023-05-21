using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class GameUObjectSetPropertyCommandTests
{
    [Fact]
    public void SuccessfulGameUObjectSetPropertyCommandExecute()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var obj = new Mock<IUObject>();
        obj.Setup(o => o.setProperty(It.IsAny<string>(), It.IsAny<object>())).Callback(() => {}).Verifiable();

        var gameUObjectSetPropertyCommand = new GameSetPropertiesCommand(obj.Object, "key", 1);

        gameUObjectSetPropertyCommand.Execute();

        obj.VerifyAll();
    }
}
