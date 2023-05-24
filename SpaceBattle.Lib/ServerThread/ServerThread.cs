namespace SpaceBattle.Lib;
using Hwdtech;
public class ServerThread
{
    private bool stop = false;
    internal IReciever queue;

    internal Action strategy;
    private object scope;
    internal Thread thread;
    public ServerThread(IReciever reciever, object scope)
    {
        queue = reciever;
        this.scope = scope;
        strategy = () =>
        {
            HandleCommand();
        };
        thread = new Thread(() =>
        {
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();
            while (!stop)
            {
                try
                {
                    strategy();
                }
                catch (Exception e)
                {
                    IoC.Resolve<ICommand>("Exception Handler.Add", e).Execute();
                }
            }
        }
         );

    }

    public void StartThread()
    {
        thread.Start();
    }

    internal void StopThread()
    {
        stop = true;

    }


    internal void HandleCommand()
    {
        ICommand command = queue.Recieve();
        command.Execute();
    }
}
