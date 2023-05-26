using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;


public class DeleteNewGameCommandTests
{
    [Fact]
    public void Execute_RemovesGameScopeFromGameScopeMap()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        int gameId = 123;

        var mockCommand = new Mock<ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockCommandInj = new Mock<IInjectable>();
        mockCommandInj.Setup(x => x.Inject(mockCommand.Object));

        var gameMapMock = new Mock<IStrategy>();
        gameMapMock.Setup(x => x.RunStrategy()).Returns(new Dictionary<int, IInjectable>{ { gameId, mockCommandInj.Object } });

        
        var gameEmptyCommandMock = new Mock<IStrategy>();
        gameEmptyCommandMock.Setup(x => x.RunStrategy(It.IsAny<ICommand>())).Returns(false).Verifiable();

        var mockDictionary = new Mock<IStrategy>();
        mockDictionary.Setup(x => x.RunStrategy()).Returns(new Dictionary<int, object> { { gameId, IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))) } });

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameScopeMap", (object[] args) => mockDictionary.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameMap", (object[] args) => gameMapMock.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register","GameEmptyCommand", (object[] args) => gameEmptyCommandMock.Object.RunStrategy(args)).Execute();

        var map = IoC.Resolve<IDictionary<int, SpaceBattle.Lib.IInjectable>>("GameMap");
        var empty = IoC.Resolve<ICommand>("GameEmptyCommand");
        var mapScope = IoC.Resolve<IDictionary<int, object>>("GameScopeMap");
        var deleteNewGameCommand = new DeleteNewGameCommand(gameId);
        deleteNewGameCommand.Execute();

        Assert.Empty(mapScope);
    }
}
