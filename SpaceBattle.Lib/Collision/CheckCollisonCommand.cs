using Hwdtech;

namespace SpaceBattle.Lib;

public class CheckCollisionCommand: ICommand
{
    private IUObject obj1, obj2;
    public CheckCollisionCommand(IUObject obj1, IUObject obj2)
    {
        this.obj1 = obj1;
        this.obj2 = obj2;
    }

    public void execute()
    {
        var tree = IoC.Resolve<IDictionary<int, object>>("Game.GetSolutionTree");

        var object1 = IoC.Resolve<List<int>>("Game.Command.GetProperty", obj1);
        var object2 = IoC.Resolve<List<int>>("Game.Command.GetProperty", obj2);

        var objects = IoC.Resolve<List<int>>("Game.TrainingDataToCollision", object1, object2);

        objects.ForEach(num => tree = (IDictionary<int, object>) tree[num]);

        if (tree.Keys.First() == 1) IoC.Resolve<ICommand>("Game.Collision", obj1, obj2).execute();
    }
}