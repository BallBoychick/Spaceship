namespace SpaceBattle.Lib;

public class StopThreadCommand : ICommand
{
    private ServerThread thread;

    internal Action strategy;
    public StopThreadCommand(ServerThread thread, Action? strategy = null)
    {
        this.strategy = strategy ?? (() => { thread.StopThread(); });
        this.thread = thread;

    }
    public void Execute()
    {
        if (Thread.CurrentThread == thread.thread)
        {
            strategy();
        }
        else
        {
            throw new Exception();
        }

    }

}
