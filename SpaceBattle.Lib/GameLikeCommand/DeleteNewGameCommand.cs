using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class DeleteNewGameCommand : ICommand
{
    private int gameId;

    public DeleteNewGameCommand(int gameId)
    {
        this.gameId = gameId;
    }

    public void Execute()
    {
        var gameMap = IoC.Resolve<IDictionary<int, IInjectable>>("GameMap");
        var gameCommand = gameMap[gameId];
        gameCommand.Inject(IoC.Resolve<ICommand>("GameEmptyCommand"));
        var gameScopeMap = IoC.Resolve<IDictionary<int, object>>("GameScopeMap");
        gameScopeMap.Remove(gameId);
    }
}
