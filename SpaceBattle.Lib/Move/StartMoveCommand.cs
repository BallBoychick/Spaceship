using Hwdtech;

namespace SpaceBattle.Lib;

public class StartMoveCommand : ICommand
{
    private IMoveStartable starttable;

    public StartMoveCommand(IMoveStartable starttable)
    {
        this.starttable = starttable;
    }
    public void Execute()
    {
        starttable.Properties.ToList().ForEach(a => IoC.Resolve<ICommand>("Game.Commands.SetProperty", starttable.Target, a.Key, a.Value).Execute());

        ICommand cmd = IoC.Resolve<ICommand>("Game.Operations.Movement", starttable.Target);
        IoC.Resolve<ICommand>("Game.Commands.SetProperty", starttable.Target, "Movement", cmd).Execute();
        IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<Queue<ICommand>>("Game.Queue"), cmd).Execute();

    }
}
