namespace SpaceBattle.Lib;

class ChangeBehaviorStopThreadCommand : ICommand
{
    private Action strategy;

    private StopThreadCommand stopThreadCommand;
    public ChangeBehaviorStopThreadCommand(Action strategy, StopThreadCommand command)
    {
        this.strategy = strategy;
        this.stopThreadCommand = command;
    }
    public void Execute()
    {
        stopThreadCommand.strategy = strategy;
    }
}
