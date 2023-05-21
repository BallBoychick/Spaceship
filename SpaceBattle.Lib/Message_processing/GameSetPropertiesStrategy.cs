using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class GameSetPropertiesStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var uobj = (IUObject)args[0];

        var key = (string)args[1];

        var value = (object)args[2];

        return new GameSetPropertiesCommand(uobj, key, value);
    }
}
