using Hwdtech;
using System;
namespace SpaceBattle.Lib;
public class GameQueuePushCommand : ICommand
{
    private int id;
    private ICommand cmd;

    public GameQueuePushCommand(int id, ICommand cmd)
    {
        this.id = id;
        this.cmd = cmd;
    }

    public void Execute()
    {
        var queue = IoC.Resolve<Queue<ICommand>>("GetQueueOfGameById", this.id);
        queue.Enqueue(cmd);
    }
}