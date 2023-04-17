using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
using System.Collections.Concurrent;

public class StartServerTests

{
    [Fact]
    public void Execute_CreatesAndStartsThreads()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        const int length = 1;
        var strtservstra = new StartAppStrategy();

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        int i = 0;
        mockCommand.Setup(x => x.Execute()).Callback(() => { i += 1; });

        var mockStrategyWithParams = new Mock<IStrategy>();
        mockStrategyWithParams.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(mockCommand.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "StartAppStrategy", (object[] args) => strtservstra.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateAndStartThreadStrategy", (object[] args) => mockStrategyWithParams.Object.RunStrategy(args)).Execute();

        IoC.Resolve<ICommand>("StartAppStrategy", length).Execute();

        Assert.Equal(length, i);
    }
}
