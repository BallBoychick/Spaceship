using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class NewStartMoveCommandStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new StartMoveCommand(IoC.Resolve<IMoveStartable>("NewGameAdapter", obj, typeof(IMoveStartable)));
    }
}
