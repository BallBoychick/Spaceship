namespace SpaceBattle.Lib;
using System.Collections.Concurrent;
using Hwdtech;

public class SoftStopThreadStrategy : IStrategy
{

    public object RunStrategy(params object[] parameters)
    {
        String idThread = (string)parameters[0];
        Action action = (parameters.Count() == 2) ? (Action)parameters[1] : () => { };
       
        var thread = IoC.Resolve<ServerThread>("Threads."+ idThread);
        var stopThreadCommand = new StopThreadCommand(thread);
        
        stopThreadCommand.ChangeBehaviorThreadTermination(() => {
            var queue = new BlockingCollection<Lib.ICommand>();
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register","Thread.Queue." + idThread,  (object[] param) => queue);
            while(!thread.queue.IsEmpty())
            {
                thread.HandleCommand();
            }
            stopThreadCommand.HandleCommand();

            action();
        });
        
        IoC.Resolve<Lib.ICommand>("Send Command", idThread, stopThreadCommand).execute();
        return true;
    }
}
