using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class NewCommandStrategysTests
{
    [Fact]
    public void SuccessfulShootRun()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var shootobj = new Mock<IShootable>();

        var adaptStrategy = new Mock<IStrategy>();
        adaptStrategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(shootobj.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "NewGameAdapter", (object[] args) => adaptStrategy.Object.RunStrategy(args)).Execute();

        var strategy = new NewShootCommandStrategy();

        var obj = new Mock<IUObject>();

        var res = strategy.RunStrategy(obj.Object);

        Assert.NotNull(res);
        Assert.IsType<ShootCommand>(res);
    }

    [Fact]
    public void SuccessfulRotateCommandRun()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var shootobj = new Mock<IRotatable>();

        var adaptStrategy = new Mock<IStrategy>();
        adaptStrategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(shootobj.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "NewGameAdapter", (object[] args) => adaptStrategy.Object.RunStrategy(args)).Execute();

        var strategy = new NewRotateCommandStrategy();

        var obj = new Mock<IUObject>();

        var res = strategy.RunStrategy(obj.Object);

        Assert.NotNull(res);
        Assert.IsType<RotateCommand>(res);
    }

    [Fact]
    public void SuccessfulNewStartMoveCommandRun()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var shootobj = new Mock<IMoveStartable>();

        var adaptStrategy = new Mock<IStrategy>();
        adaptStrategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(shootobj.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "NewGameAdapter", (object[] args) => adaptStrategy.Object.RunStrategy(args)).Execute();

        var strategy = new NewStartMoveCommandStrategy();

        var obj = new Mock<IUObject>();

        var res = strategy.RunStrategy(obj.Object);

        Assert.NotNull(res);
        Assert.IsType<StartMoveCommand>(res);
    }

    [Fact]
    public void SuccessfulRNewStartMoveCommandRun()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var shootobj = new Mock<IMoveStopable>();

        var adaptStrategy = new Mock<IStrategy>();
        adaptStrategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(shootobj.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "NewGameAdapter", (object[] args) => adaptStrategy.Object.RunStrategy(args)).Execute();

        var strategy = new NewStopMoveCommandStrategy();

        var obj = new Mock<IUObject>();

        var res = strategy.RunStrategy(obj.Object);

        Assert.NotNull(res);
        Assert.IsType<StopMoveCommand>(res);
    }
}
