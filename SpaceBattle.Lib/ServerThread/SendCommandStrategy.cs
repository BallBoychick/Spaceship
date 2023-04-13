namespace SpaceBattle.Lib;
using System.Collections.Concurrent;
using Hwdtech;

public class SendCommandStrategy : IStrategy
{

    public object RunStrategy(params object[] parameters)
    {
        String idThread = (string)parameters[0];
        ICommand command = (ICommand)parameters[1];

        var queue = IoC.Resolve<BlockingCollection<Lib.ICommand>>("Thread.Queue." + idThread);
        queue.Add(command);
        return true;
    }
}
