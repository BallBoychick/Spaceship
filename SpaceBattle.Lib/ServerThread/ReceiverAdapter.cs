namespace SpaceBattle.Lib;
using System.Collections.Concurrent;

public class ReceiverAdapter : IReciever
{
    BlockingCollection<ICommand> queue;

    public ReceiverAdapter(BlockingCollection<ICommand> queue) => this.queue = queue; 
    public bool IsEmpty()
    {
        return queue.Count == 0;
    }

    public ICommand Recieve()
    {
        return queue.Take();
    }
}
