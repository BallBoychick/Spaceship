namespace SpaceBattle.Lib;
using Hwdtech;

public class StopServer : ICommand
{
    public void Execute()
    {
        // IoC.Resolve<> ....
        // Ioc.Resolve<ICommand>("SoftStopThreadStrategy").Execute();
    }
}