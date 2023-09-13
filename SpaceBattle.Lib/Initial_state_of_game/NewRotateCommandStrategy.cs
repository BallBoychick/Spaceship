using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class NewRotateCommandStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new RotateCommand(IoC.Resolve<IRotatable>("NewGameAdapter", obj, typeof(IRotatable)));
    }
}
