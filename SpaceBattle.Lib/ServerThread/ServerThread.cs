namespace SpaceBattle.Lib;
using Hwdtech;
public class ServerThread
{
    private bool stop = false;
    internal IReciever queue;

    private Action strategy;
    internal Thread thread;
    public ServerThread(IReciever reciever)
    {
        queue = reciever;

        strategy = () =>
        {
            HandleCommand();
        };
        thread = new Thread(() =>
        {
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
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

    public void StopThread()
    {
        stop = true;

    }

    internal void ChangeBehaviour(Action changeBehaviour)
    {
        strategy = changeBehaviour;
    }

    internal void HandleCommand()
    {
        ICommand command = queue.Recieve();
        command.Execute();
    }
}
