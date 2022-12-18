using Hwdtech;

namespace SpaceBattle.Lib;

public class StopMoveCommand : ICommand
{
    private IMoveStopable stoppable;

    public StopMoveCommand(IMoveStopable stoppable)
    {
        this.stoppable = stoppable;
    }
    
    public void execute()
    {
        stoppable.Properties.ToList().ForEach(a => IoC.Resolve<ICommand>("Game.Commands.RemoveProperty", stoppable.Target, a).execute());

        IoC.Resolve<IInjectable>("Game.Commands.SetupCommand", stoppable.Target, "Movement").Injecting(IoC.Resolve<ICommand>("Game.Commands.Empty"));
    }
}