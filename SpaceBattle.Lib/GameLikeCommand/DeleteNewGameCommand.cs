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
        var gameScopeMap = IoC.Resolve<IDictionary<int, object>>("GameScopeMap");
        gameScopeMap.Remove(gameId);
    }
}
