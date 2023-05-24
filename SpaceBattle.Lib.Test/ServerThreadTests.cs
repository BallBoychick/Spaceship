namespace SpaceBattle.Lib.Test;
using Moq;
using System;
using Xunit;
using Hwdtech.Ioc;
using Hwdtech;
using SpaceBattle.Lib;
using System.Threading;

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
            var sender = IoC.Resolve<ISender>("Sender" + (string)param[0]);
            return new SendCommand(sender, (Lib.ICommand)param[1]);
        }).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Hard Stop Command", (object[] param) => {
            var thread = IoC.Resolve<ServerThread>("Threads." + (string)param[0]);
            return new HardStopThreadCommand(thread, (Action)param[1]);
            }).Execute();
        // IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Soft Stop Command", (object[] param) =>
        // {
        //     var softStopThreadCommand = new Mock<Lib.ICommand>();
        //     softStopThreadCommand.Setup(x => x.Execute()).Callback(() => new SoftStopThreadStrategy().RunStrategy(param)); return softStopThreadCommand.Object;
        // }).Execute();




    }

    [Fact]
    public void StartThreadSuccess()
    {
        InitState();
        Action action = () => {};
        var mre = new ManualResetEvent(false);
        var mockCommand = new Mock<Lib.ICommand>();
        mockCommand.Setup(x => x.Execute()).Callback(() => mre.Set()).Verifiable();
        var startThreadCommand = IoC.Resolve<Lib.ICommand>("Create And Start Thread", "1");
        startThreadCommand.Execute();
        IoC.Resolve<Lib.ICommand>("Send Command", "1", mockCommand.Object).Execute();

        var isDone = mre.WaitOne(10000);
        var thread = IoC.Resolve<ServerThread>("Threads.1");

        var cmd = IoC.Resolve<Lib.ICommand>("Hard Stop Command", "1", action);
        cmd.Execute();
        Assert.True(isDone);
        mockCommand.Verify();
    }
    [Fact]
    public void HardStopThreadCommandSuccess()
    {
        InitState();
        var mreOne = new ManualResetEvent(false);
        var mockCommandOne = new Mock<Lib.ICommand>();
        mockCommandOne.Setup(x => x.Execute()).Callback(() => mreOne.Set()).Verifiable();
        var startThreadCommand = IoC.Resolve<Lib.ICommand>("Create And Start Thread", "1");
        startThreadCommand.Execute();
        IoC.Resolve<Lib.ICommand>("Send Command", "1", mockCommandOne.Object).Execute();

        var isDoneFirstCommand = mreOne.WaitOne(10000);
        Assert.True(isDoneFirstCommand);
        var mreHardStop = new ManualResetEvent(false);
        var mreTwo = new ManualResetEvent(false);
        var cmd = IoC.Resolve<Lib.ICommand>("Hard Stop Command", "1", () => { mreHardStop.Set(); });
        cmd.Execute();
        var mockCommandTwo = new Mock<Lib.ICommand>();
        mockCommandTwo.Setup(x => x.Execute()).Callback(() => { mreTwo.Set(); });
        IoC.Resolve<Lib.ICommand>("Send Command", "1", mockCommandTwo.Object).Execute();
        var isDoneHardStop = mreHardStop.WaitOne(10000);

        Assert.True(isDoneHardStop);
        var isDoneCommandTwo = mreTwo.WaitOne(10000);
        Assert.False(isDoneCommandTwo);
    }
    [Fact]
    public void SoftStopThreadCommandSuccess()
    {
        InitState();
        var mreOne = new ManualResetEvent(false);
        var mockCommandOne = new Mock<Lib.ICommand>();
        mockCommandOne.Setup(x => x.Execute()).Callback(() => mreOne.Set()).Verifiable();
        var startThreadCommand = IoC.Resolve<Lib.ICommand>("Create And Start Thread", "1", () => { });
        startThreadCommand.Execute();
        IoC.Resolve<Lib.ICommand>("Send Command", "1", mockCommandOne.Object).Execute();

        var isDoneFirstCommand = mreOne.WaitOne(10000);
        Assert.True(isDoneFirstCommand);
        var mreSoftStop = new ManualResetEvent(false);
        var mreTwo = new ManualResetEvent(false);
        var cmd = IoC.Resolve<Lib.ICommand>("Soft Stop Command", "1", () => { mreSoftStop.WaitOne(10000); });
        cmd.Execute();
        var mockCommandTwo = new Mock<Lib.ICommand>();
        mockCommandTwo.Setup(x => x.Execute()).Callback(() => { mreTwo.Set(); });
        IoC.Resolve<Lib.ICommand>("Send Command", "1", mockCommandTwo.Object).Execute();
        mreSoftStop.Set();
        var isDoneCommandTwo = mreTwo.WaitOne(10000);
        Assert.True(isDoneCommandTwo);

        var mreThree = new ManualResetEvent(false);
        var mockCommandThree = new Mock<Lib.ICommand>();
        mockCommandTwo.Setup(x => x.Execute()).Callback(() => { mreThree.Set(); });
        IoC.Resolve<Lib.ICommand>("Send Command", "1", mockCommandThree.Object).Execute();

        var isDoneCommandThree = mreThree.WaitOne(10000);
        Assert.False(isDoneCommandThree);

    }

    [Fact]

    public void ThreadStopAnoutherThread()
    {

        InitState();
        Action action = () => { };
        var startFirstThreadCommand = IoC.Resolve<Lib.ICommand>("Create And Start Thread", "1");
        startFirstThreadCommand.Execute();
        var thread1 = IoC.Resolve<ServerThread>("Threads.1");
        var mockRegisterExceptionHandler = new Mock<Lib.ICommand>();
        var exceptionHandlerDict = new Dictionary<string, object>();
        var MRE = new ManualResetEvent(false);
        mockRegisterExceptionHandler.Setup(x => x.Execute()).Callback(() =>
        {
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Exception Handler.Add", (object[] param) =>
            {
                var mockCMD = new Mock<Lib.ICommand>();
                mockCMD.Setup(x => x.Execute()).Callback(() =>
                {
                    exceptionHandlerDict.Add("Exception in Thread", (Exception)param[0]);
                });


                return mockCMD.Object;
            }).Execute();
            MRE.Set();
        });
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Exception Handler", (object[] param) =>
        {
            return exceptionHandlerDict;
        }).Execute();

        IoC.Resolve<Lib.ICommand>("Send Command", "1", mockRegisterExceptionHandler.Object).Execute();
        MRE.WaitOne(10000);
        var startSecondThreadCommand = IoC.Resolve<Lib.ICommand>("Create And Start Thread", "2");
        startSecondThreadCommand.Execute();
        var thread2 = IoC.Resolve<ServerThread>("Threads.2");
        var mre = new ManualResetEvent(false);
        var mockCommandNotifyAndStop = new Mock<Lib.ICommand>();
        var stopThreadCommand = new StopThreadCommand(thread2, action);
        mockCommandNotifyAndStop.Setup(x => x.Execute()).Callback(() =>
        {
            stopThreadCommand.Execute();
            mre.Set();
        });

        IoC.Resolve<Lib.ICommand>("Send Command", "1", mockCommandNotifyAndStop.Object).Execute();
        mre.WaitOne(10000);
        var exceptionHandler = IoC.Resolve<Dictionary<string, object>>("Exception Handler");

        var wasException = exceptionHandler.ContainsKey("Exception in Thread");

        Assert.True(wasException);

        IoC.Resolve<Lib.ICommand>("Hard Stop Command", "1").Execute();

        IoC.Resolve<Lib.ICommand>("Soft Stop Command", "2").Execute();
    }
}
