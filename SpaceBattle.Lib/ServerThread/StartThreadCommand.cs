namespace SpaceBattle.Lib;

public class StartThreadCommand : ICommand
{
    private ServerThread thread;
    private Action action;

    public StartThreadCommand(ServerThread thread, Action action)
    {
        this.thread = thread;
        this.action = action;
    }
    public void Execute()
    {
        thread.StartThread();
        action();

    }
}
