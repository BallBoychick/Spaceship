namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class StopServer : ICommand
{
    public void Execute()
    {
        // IoC.Resolve<> ....
        // Ioc.Resolve<ICommand>("SoftStopThreadStrategy").Execute();
        IoC.Resolve<ConcurrentDictionary<int, ISender>>("SenderMap").ToList().ForEach(
            item => IoC.Resolve<ICommand>("SendCommandStrategy",
                item.Key, IoC.Resolve<ICommand>("SoftStopServerThreadCommandStrategy", item.Key)).Execute()
        );
    }
}