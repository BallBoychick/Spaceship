using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
public class GameRegisterOfOperationStrategyTests
{
    [Fact]
    public void SuccessfulShootRun()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var id = 1;

        var strategy = new GameRegisterOfOperationCommandStrategy();

        var res = strategy.RunStrategy(id);

        Assert.NotNull(res);

        Assert.IsType<GameRegisterOfOperationCommand>(res);
    }
}
