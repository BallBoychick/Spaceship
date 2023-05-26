namespace SpaceBattle.Lib;
using System.Collections.Concurrent;
using Hwdtech;

public class CreateStartServerThreadStrategy : IStrategy
{

    public object RunStrategy(params object[] parameters)
    {
        String idThread = (string)parameters[0];
        Action action = (parameters.Count() == 2) ? (Action)parameters[1] : () => { };
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        Action actionStartThread = ()=> {
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();
            action();
        };
        var actioncmd = new ActionCommand(actionStartThread);
        var blockingCollection = new BlockingCollection<Lib.ICommand>();
        var reciever = new ReceiverAdapter(blockingCollection);
        var sender = new SenderAdapter(blockingCollection);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Sender" + idThread, (object[] param) => sender).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Reciever" + idThread, (object[] param) => reciever).Execute();
        
        var thread = new ServerThread(reciever, scope);
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Threads." + idThread, (object[] param) =>
        {
            return thread;
        }).Execute();
        var startThreadCommand = new StartThreadCommand(thread);
        IoC.Resolve<Lib.ICommand>("Send Command", idThread, actioncmd).Execute();
        return startThreadCommand;
    }
}
