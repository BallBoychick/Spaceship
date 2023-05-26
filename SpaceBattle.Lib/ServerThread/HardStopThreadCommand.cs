namespace SpaceBattle.Lib;

public class HardStopThreadCommand : ICommand
{
    private ServerThread thread;
    internal Action action = () => { };
    public HardStopThreadCommand(ServerThread thread, Action action)
    {
        this.thread = thread;
        this.action = action;
    }
    public void Execute()
    {
        if (Thread.CurrentThread == thread.thread)
        {
            thread.StopThread();
            action();
        }
        else
        {
            throw new Exception();
        }
    }
}
