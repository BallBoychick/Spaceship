namespace SpaceBattle.Lib;
using System.Diagnostics;
using Hwdtech;
public class GameCommand : ICommand
{
    private IReciever Queue;
    private object Scope;
    private Stopwatch stopwatch;
    public GameCommand(IReciever Queue, object Scope)
    {
        this.Queue = Queue;
        this.Scope = Scope;
        this.stopwatch = new Stopwatch();
        

    }

    public void Execute()
    {
        var quant = IoC.Resolve<double>("Quant");
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", Scope).Execute();
        stopwatch.Start();
        do
        {
            if (Queue.IsEmpty())
            {
                break;
            }
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
        while (quant <= this.stopwatch.ElapsedMilliseconds);
        stopwatch.Reset();
    }
}
