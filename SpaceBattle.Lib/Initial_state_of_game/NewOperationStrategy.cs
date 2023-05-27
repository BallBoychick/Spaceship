using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class NewOperationStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var obj = (IUObject)args[0];
        var orderType = (string)args[1];

        var listOfRules = IoC.Resolve<IEnumerable<string>>("GameRules." + orderType);

        var commandList = listOfRules.ToList().Select(rule => IoC.Resolve<ICommand>("CreateGameCommand." + rule, obj));

        return IoC.Resolve<ICommand>("CreateGameMacroCommand", commandList);
    }
}
