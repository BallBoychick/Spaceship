using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;

public class GameRegisterOfOperationCommandTests
{
    [Fact]
    public void GameRegisterOfOperationCommandRun()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var queue = new Queue<ICommand>();

        var cmd = new Mock<SpaceBattle.Lib.ICommand>();
        cmd.Setup(x => x.Execute()).Callback(() => {});

        var checkCommandStrategy = new Mock<IStrategy>();

        checkCommandStrategy.Setup(x => x.RunStrategy()).Returns(cmd.Object).Verifiable();

        var pushCommand = new Mock<ICommand>();
        pushCommand.Setup(c => c.Execute()).Callback(() => queue.Enqueue(cmd.Object)).Verifiable();

        var pushCommandStrategy = new Mock<IStrategy>();

        pushCommandStrategy.Setup(x => x.RunStrategy(It.IsAny<int>(), It.IsAny<ICommand>())).Returns(pushCommand.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GamesThreadRgisterOperationStrategy", (object[] args) => checkCommandStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameQueuePushStrategy", (object[] args) => pushCommandStrategy.Object.RunStrategy(args)).Execute();



        var id = 1;

        var gameregcomm = new GameRegisterOfOperationCommand(id);

        gameregcomm.Execute();

        checkCommandStrategy.Verify();
        pushCommand.Verify();
        pushCommandStrategy.Verify();
    }
}
