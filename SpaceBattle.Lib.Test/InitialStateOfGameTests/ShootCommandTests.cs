using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class ShootCommandTests
{
    [Fact]
    public void ShootCommandRun()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        var queue = new Queue<ICommand>();

        var cmd = new Mock<SpaceBattle.Lib.ICommand>();

        var obj = new Mock<object>();
        var getid = new Mock<IStrategy>();
        getid.Setup(s => s.RunStrategy()).Returns(1).Verifiable();
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameQueuePushStrategy", (object[] args) => new ActionCommand(() =>
        queue.Enqueue((SpaceBattle.Lib.ICommand)args[1]))).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetGameQueueOfGameById", (object[] args) => getid.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Create.Projectile", (object[] args) => obj.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Create.Projectile.Command.Move", (object[] args) => cmd.Object).Execute();



        var obj2 = new Mock<IShootable>();
        var ShootCommand = new ShootCommand(obj2.Object);
        ShootCommand.Execute();

        Assert.True(queue.Count() == 1);
    }
}
