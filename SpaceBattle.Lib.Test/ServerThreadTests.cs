namespace SpaceBattle.Lib.Test;
using Moq;
using System;
using Xunit;
using Hwdtech.Ioc;
using Hwdtech;
using SpaceBattle.Lib;

public class ServerThreadTests
{
    public ServerThreadTests()
    {

    }
    private void InitState()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Create And Start Thread", (object[] param) => new CreateStartServerThreadStrategy().RunStrategy(param)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Send Command", (object[] param) =>
        {
            var sendCommand = new Mock<Lib.ICommand>();
            sendCommand.Setup(x => x.execute()).Callback(() => new SendCommandStrategy().RunStrategy(param)); return sendCommand.Object;
        }).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Hard Stop Command", (object[] param) =>
        {
            var hardStopThreadCommand = new Mock<Lib.ICommand>();
            hardStopThreadCommand.Setup(x => x.execute()).Callback(() => new HardStopThreadStrategy().RunStrategy(param)); return hardStopThreadCommand.Object;
        }).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Soft Stop Command", (object[] param) =>
        {
            var softStopThreadCommand = new Mock<Lib.ICommand>();
            softStopThreadCommand.Setup(x => x.execute()).Callback(() => new SoftStopThreadStrategy().RunStrategy(param)); return softStopThreadCommand.Object;
        }).Execute();





    }

    [Fact]
    public void StartThreadSuccess()
    {
        InitState();
        var mre = new ManualResetEvent(false);
        var mockCommand = new Mock<Lib.ICommand>();
        mockCommand.Setup(x => x.execute()).Callback(() => mre.Set()).Verifiable();
        var startThreadCommand = IoC.Resolve<Lib.ICommand>("Create And Start Thread", "1");
        startThreadCommand.execute();
        new ChangeBehaviorThreadStrategy().RunStrategy("1", () => {});
        new ChangeBehaviorThreadStrategy().RunStrategy("1");
        IoC.Resolve<Lib.ICommand>("Send Command", "1", mockCommand.Object).execute();

        var isDone = mre.WaitOne(10000);
        var thread = IoC.Resolve<ServerThread>("Threads.1");
        
        var cmd = IoC.Resolve<Lib.ICommand>("Hard Stop Command", "1");
        cmd.execute();
        Assert.True(isDone);
        mockCommand.Verify();
    }
}