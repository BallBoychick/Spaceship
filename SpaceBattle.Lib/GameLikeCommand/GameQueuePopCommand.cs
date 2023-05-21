using Hwdtech;
using System;
namespace SpaceBattle.Lib;
public class GameQueuePopCommand : ICommand
{
    private int id;
    private ICommand cmd;

    public GameQueuePopCommand(int id, ICommand cmd)
    {
        this.id = id;
        this.cmd = cmd;
    }

    public void Execute()
    {
        var queue = IoC.Resolve<Queue<ICommand>>("GetQueueOfGameById", this.id);
        queue.Dequeue();
    }
}
