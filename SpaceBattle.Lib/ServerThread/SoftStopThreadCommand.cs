namespace SpaceBattle.Lib;

public class SoftStopThreadCommand : ICommand
{
    private ServerThread thread;
    internal Action action = () => { };
    public SoftStopThreadCommand(ServerThread thread, Action action)
    {
        this.thread = thread;
        this.action = action;
    }
    public void Execute()
    {
        var queue = thread.queue;
        new ChangeBehaviorThreadCommand(() =>
        {
            while (!queue.IsEmpty())
            {
                queue.Recieve().Execute();
            }
            new HardStopThreadCommand(thread, action).Execute();
        }, thread).Execute();
    }
}
