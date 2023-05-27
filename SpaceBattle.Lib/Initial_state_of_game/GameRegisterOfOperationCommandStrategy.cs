using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class GameRegisterOfOperationCommandStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var gameID = (int)args[0];
        return new GameRegisterOfOperationCommand(gameID);
    }
}
