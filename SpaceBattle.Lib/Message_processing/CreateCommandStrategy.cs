using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class CreateCommandStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var message = (IMessage)args[0];

        var OrderType = message.OrderType;

        var gameitemid = message.GameItemID;

        var properties = message.Properties;

        var uobj = IoC.Resolve<IUObject>("GetUObjFromMap", gameitemid);

        properties.ToList().ForEach(x => IoC.Resolve<ICommand>("GameSetProperties", uobj, x.Key, x.Value).Execute());

        return IoC.Resolve<ICommand>("CreateCommand." + OrderType, uobj);
    }
}