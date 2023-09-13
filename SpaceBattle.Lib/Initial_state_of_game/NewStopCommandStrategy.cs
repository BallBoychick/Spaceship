using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class NewStopMoveCommandStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new StopMoveCommand(IoC.Resolve<IMoveStopable>("NewGameAdapter", obj, typeof(IMoveStopable)));
    }
}
