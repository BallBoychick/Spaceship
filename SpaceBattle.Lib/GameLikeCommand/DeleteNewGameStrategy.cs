using Hwdtech;
using System;

namespace SpaceBattle.Lib;

public class DeleteNewGameStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        return new DeleteNewGameCommand((int)args[0]);
    }
}
