namespace SpaceBattle.Lib;

class ChangeBehaviorThreadCommand : ICommand
{
    private Action strategy;

    private ServerThread thread;
    public ChangeBehaviorThreadCommand(Action strategy, ServerThread thread)
    {
        this.strategy = strategy;
        this.thread = thread;
    }
    public void Execute()
    {
        thread.strategy = strategy;
    }
}
