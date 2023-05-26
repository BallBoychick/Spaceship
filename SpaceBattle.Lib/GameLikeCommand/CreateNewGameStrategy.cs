using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class CreateNewGameStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var gameId = (int)args[0];
        var scope = IoC.Resolve<Object>("GameScope");
        var gameQueue = new Queue<ICommand>();

        var gameLikeCommand = IoC.Resolve<ICommand>("GameCommand", gameQueue, scope);
        var gameMap = IoC.Resolve<IDictionary<int, ICommand>>("GameMap");

        IEnumerable<ICommand> commandList = new List<ICommand>();
        var macroCommand = IoC.Resolve<ICommand>("MacroGameCommand", commandList);
        var injectCommand = IoC.Resolve<ICommand>("InjectGameCommand", macroCommand);
        var repeatCommand = IoC.Resolve<ICommand>("RepeatGameCommand", injectCommand);

        commandList.Append(repeatCommand);

        gameMap.Add(gameId, injectCommand);
        return injectCommand;

    }
}
