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
        var proj = IoC.Resolve<object>("Game.Create.Projectile", obj.ProjectileType);
        var cmd = IoC.Resolve<ICommand>("Game.Create.Projectile.Command.Move", proj, obj.Pos, obj.Velocity);
        IoC.Resolve<SpaceBattle.Lib.ICommand>("GameQueuePushStrategy", IoC.Resolve<int>("GetGameQueueOfGameById"), cmd).Execute();
    }
}
