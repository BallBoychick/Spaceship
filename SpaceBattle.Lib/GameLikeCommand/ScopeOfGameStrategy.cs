using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class ScopeOfGameStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var gameId = (int)args[0];
        var parentScope = (object)args[1];
        var quantum = (double)args[2];

        var gameScopeMap = IoC.Resolve<IDictionary<int, object>>("GameScopeMap");

        var scope = IoC.Resolve<object>("Scopes.New", parentScope);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameQueuePushStrategy", (object[] args) => new GameQueuePushStrategy().RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameQueuePopStrategy", (object[] args) => new GameQueuePopStrategy().RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUobjectFromMapStrategy", (object[] args) => new GetUobjectFromMapStrategy().RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "RemoveUObjectFromMapStrategy", (object[] args) => new RemoveUObjectFromMapStrategy().RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetQuantum", (object[] args) => (object)quantum).Execute();

        gameScopeMap.Add(gameId, scope);
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", parentScope).Execute();

        return gameScopeMap;
    }
}
