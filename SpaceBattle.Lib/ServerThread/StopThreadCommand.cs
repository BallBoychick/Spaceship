namespace SpaceBattle.Lib;

public class StopThreadCommand: ICommand
{
    private ServerThread thread;

    private Action strategy;
    public StopThreadCommand(ServerThread thread) 
    {
        strategy = () => {    HandleCommand();};
        this.thread = thread;
        
    }
    public void execute()
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
    internal void HandleCommand()
    {
        thread.StopThread();
    }

    internal void ChangeBehaviorThreadTermination(Action action )
    {
        this.strategy = action;
    }
}
