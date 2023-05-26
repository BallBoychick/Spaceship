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
            while (!stop)
            {
                strategy();
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

        try
        {
            command.Execute();
        }
        catch (Exception e)
        {
            IoC.Resolve<ICommand>("Exception Handler", e, command).Execute();
        }
    }
}
