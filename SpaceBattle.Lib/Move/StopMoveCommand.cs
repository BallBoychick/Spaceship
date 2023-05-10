using Hwdtech;

namespace SpaceBattle.Lib;

public class StopMoveCommand : ICommand
{
    private IMoveStopable stoppable;

    public StopMoveCommand(IMoveStopable stoppable)
    {
        this.stoppable = stoppable;
    }

    public void Execute()
    {
        stoppable.Properties.ToList().ForEach(a => IoC.Resolve<ICommand>("Game.Commands.RemoveProperty", stoppable.Target, a).Execute());

        IoC.Resolve<IInjectable>("Game.Commands.SetProperty", stoppable.Target, "Movement").Inject(IoC.Resolve<ICommand>("Game.Commands.Empty"));
    }
}
