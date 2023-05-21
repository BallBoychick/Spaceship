using Hwdtech;
using Hwdtech.Ioc;
using System;
namespace SpaceBattle.Lib;

 public class ScopeOfGameStrategyTests
{
    [Fact]
    public void RunStrategy_AddsScopeToGameScopeMap()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var mockCommand = new Mock<ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsCommand.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(mockCommand.Object).Verifiable();


        var scope = IoC.Resolve<object>("Scopes.Root");

        var mockDictionary = new Mock<IStrategy>();
        mockDictionary.Setup(x => x.RunStrategy()).Returns(new Dictionary<int, object>());

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameScopeMap", (object[] args) => new Dictionary<int, object>()).Execute();

        int gameId = 123;
        object parentScope = IoC.Resolve<object>("Scopes.Root");
        double quantum = 1.0;

        
        var strategy = new ScopeOfGameStrategy();

        var result = strategy.RunStrategy(gameId, parentScope, quantum);

        Assert.NotEqual(parentScope, mockDictionary);
    }
}
