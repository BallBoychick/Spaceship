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

        var mockDictionary = new Mock<IStrategy>();
        mockDictionary.Setup(x => x.RunStrategy()).Returns(new Dictionary<int, object>());
        int gameId = 123;

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameScopeMap", (object[] args) => mockDictionary.Object.RunStrategy(args)).Execute();


        var command = new DeleteNewGameCommand(gameId);

        command.Execute();

        Assert.NotNull(command);
    }
}
