using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class NewShootCommandStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new ShootCommand(IoC.Resolve<IShootable>("NewGameAdapter", obj, typeof(IShootable)));
    }
}
