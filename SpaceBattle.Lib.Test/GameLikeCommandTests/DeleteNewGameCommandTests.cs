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
        var mockCommand = new Mock<ICommand>();
        mockCommand.Setup(x => x.Execute());

        var gameMapMock = new Mock<IStrategy>();
        gameMapMock.Setup(x => x.RunStrategy()).Returns(new Dictionary<int, IInjectable>());

        // var gameEmptyCommandMock = new Mock<ICommand>();
        // gameEmptyCommandMock.Setup(x => x.Execute());
        var gameEmptyCommandMock = new Mock<IStrategy>();
        gameEmptyCommandMock.Setup(x => x.RunStrategy(It.IsAny<ICommand>())).Returns(mockCommand.Object).Verifiable();

        var mockDictionary = new Mock<IStrategy>();
        mockDictionary.Setup(x => x.RunStrategy()).Returns(new Dictionary<int, object>());
        int gameId = 123;

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameScopeMap", (object[] args) => mockDictionary.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameMap", (object[] args) => gameMapMock.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register","GameEmptyCommand", (object[] args) => gameEmptyCommandMock.Object.RunStrategy(args)).Execute(); 

        
        var deleteNewGameCommand = new DeleteNewGameCommand(gameId);
        deleteNewGameCommand.Execute();
        // var gameCommandMock = new Mock<ICommand>(); 
        // gameMapMock.Setup(m => m[gameId]).Returns(gameCommandMock.Object);
        // var gameCommandMock = new Mock<ICommand>(); 
        // var strategyMock = new Mock<IStrategy>(); 
        // gameMapMock.Setup(m => m[gameId]).Returns(strategyMock.Object);

        //     var command = new DeleteNewGameCommand(gameId);

        //     command.Execute();

        //     Assert.NotNull(command);
    }
}
