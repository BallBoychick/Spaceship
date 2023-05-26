namespace SpaceBattle.Lib;
using System.Diagnostics;
using Hwdtech;
public class GameCommand : ICommand
{
    private IReciever Queue;
    private object Scope;
    public GameCommand(IReciever Queue, object Scope)
    {
        this.Queue = Queue;
        this.Scope = Scope;

    }

    public void Execute()
    {
      
        var stopWatch = new Stopwatch();
        var quant = IoC.Resolve<double>("Quant");
        stopWatch.Start();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Scope).Execute();
        do
        {
            ICommand command = Queue.Recieve();
            try
            {

                command.Execute();
            }
            catch (Exception e)
            {
                IoC.Resolve<ICommand>("Exception Handler", e, command).Execute();
            }

        }
        while (quant < stopWatch.ElapsedTicks);
        stopWatch.Stop();
    }
}