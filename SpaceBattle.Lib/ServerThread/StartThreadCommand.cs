namespace SpaceBattle.Lib;

public class StartThreadCommand : ICommand
{
    private ServerThread thread;

    public StartThreadCommand(ServerThread thread)
    {
        this.thread = thread;
    }
    public void Execute()
    {
        thread.StartThread();

    }
}
