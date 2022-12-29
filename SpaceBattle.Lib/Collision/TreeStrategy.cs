using Hwdtech;

namespace SpaceBattle.Lib;

public class TreeStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var tree = IoC.Resolve<IDictionary<int, object>>("Game.GetSolutionTree");
        var objects = (List<int>)args[0];
        objects.ForEach(num => tree = (IDictionary<int, object>)tree[num]);

        if (tree.Keys.First() == 1)
        {
            return true;
        }
        return false;
    }
}