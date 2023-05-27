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
        cmd.Setup(x => x.Execute()).Callback(() => {});

        var pushCommand = new Mock<ICommand>();
        pushCommand.Setup(c => c.Execute()).Callback(() => queue.Enqueue(cmd.Object)).Verifiable();

        var queueStrategy = new Mock<IStrategy>();
        queueStrategy.Setup(x => x.RunStrategy()).Returns(queue).Verifiable();

        var shootStrategy = new Mock<IStrategy>();
        shootStrategy.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(cmd.Object).Verifiable();

        var pushStrategy = new Mock<IStrategy>();

        pushStrategy.Setup(x => x.RunStrategy(It.IsAny<Queue<ICommand>>(), It.IsAny<ICommand>())).Returns(pushCommand.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameQueueGetStrategy", (object[] args) => queueStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameShootStrategy", (object[] args) => shootStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameQueuePushStrategy", (object[] args) => pushStrategy.Object.RunStrategy(args)).Execute();



        var obj = new Mock<IShootable>();
        var ShootCommand = new ShootCommand(obj.Object);
        ShootCommand.Execute();

        Assert.True(queue.Count() == 1);
        queueStrategy.VerifyAll();
        pushCommand.VerifyAll();
        shootStrategy.Verify();
        pushStrategy.Verify();
    }
}
