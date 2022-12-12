using Hwdtech;

namespace SpaceBattle.Lib;

class StopMoveCommand : ICommand
{
    IMoveStopable stopable;

    public void execute()
    {
        IoC.Resolve<ICommand>("Game.Commands.DeleteProperty", stopable.Target).execute();

        IoC.Resolve<IInjectable>("Game.Commands.SetupCommand", stopable.Target, "Movement").Injecting(IoC.Resolve<ICommand>("Game.Commands.Empty"));
        //вроде норм, а вроде нееет
    }
}