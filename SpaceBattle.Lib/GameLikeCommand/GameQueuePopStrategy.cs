namespace SpaceBattle.Lib;
using Hwdtech;
public class GameQueuePopStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var id = (int)args[0];
        var cmd = (ICommand)args[1];

        var queue = IoC.Resolve<Queue<ICommand>>("GetQueueOfGameById", id);

        return queue.Dequeue();
    }
}
