using Hwdtech;

namespace SpaceBattle.Lib;

class StartMoveCommand : ICommand
{
    IMoveStartable starttable;

    public void execute()
    {
        IoC.Resolve<ICommand>("Game.Commands.SetProperty", "starttable.Target", "Velocity", "starttable.Properties").execute();
        ICommand cmd = IoC.Resolve<ICommand>("Game.Operations.Moving", starttable.Target);
        IoC.Resolve<ICommand>("Game.Commands.SetProperty", starttable.Target, "Movement", cmd).execute();
        // IoC.Resolve<Queue>("Game.Queue").Push(cmd); //типа надо пуш реализовать?
        //а можно ли этот пуш как-то заменить на ioc.resolve("Game.Queue.Push")?????
        IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<Queue<ICommand>>("Game.Queue"), cmd).execute();
        
    }
}