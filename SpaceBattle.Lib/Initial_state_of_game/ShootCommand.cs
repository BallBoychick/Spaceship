using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class ShootCommand : ICommand
{
    private IShootable obj;

    public ShootCommand(IShootable obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        IoC.Resolve<ICommand>("GameQueuePushStrategy", IoC.Resolve<Queue<ICommand>>("GameQueueGetStrategy"), IoC.Resolve<ICommand>("GameShootStrategy", obj)).Execute();
    }
}
