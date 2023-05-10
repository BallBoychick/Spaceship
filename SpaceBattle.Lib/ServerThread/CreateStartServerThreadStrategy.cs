namespace SpaceBattle.Lib;
using System.Collections.Concurrent;
using Hwdtech;

public class CreateStartServerThreadStrategy : IStrategy
{

    public object RunStrategy(params object[] parameters)
    {
        String idThread = (string)parameters[0];
        Action action = (parameters.Count() == 2) ? (Action)parameters[1] : () => { };

        var blockingCollection = new BlockingCollection<Lib.ICommand>();
        var reciever = new ReceiverAdapter(blockingCollection);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Thread.Queue." + idThread, (object[] param) => blockingCollection).Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        var thread = new ServerThread(reciever, scope);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Threads." + idThread, (object[] param) =>
        {
            return thread;
        }).Execute();
        var startThreadCommand = new StartThreadCommand(thread, action);
        return startThreadCommand;

    }
}
