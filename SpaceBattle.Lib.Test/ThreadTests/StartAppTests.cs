using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
using System.Collections.Concurrent;

public class RunAppTest

{
    [Fact]
    public void Execute_CreatesAndStartsApp()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        const int length = 24;
        var strtservstra = new StartAppStrategy();

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockStrategyWithParams = new Mock<IStrategy>();
        mockStrategyWithParams.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(mockCommand.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "StartAppStrategy", (object[] args) => strtservstra.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateAndStartThreadStrategy", (object[] args) => mockStrategyWithParams.Object.RunStrategy(args)).Execute();

        IoC.Resolve<ICommand>("StartAppStrategy", length).Execute();


        var mockSendCommandStrategy = new Mock<ICommand>();
        var mockThreadMap = new ConcurrentDictionary<int, ISender>();
        var send = new Mock<ISender>();

        mockThreadMap[1] = send.Object;
        mockThreadMap[2] = send.Object;
        mockThreadMap[3] = send.Object;

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SenderMap", (object[] args) => mockThreadMap).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "StopServerStrategy", (object[] args) => new StopServer()).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SendCommandStrategy", (object[] args) => mockStrategyWithParams.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "SoftStopServerThreadCommandStrategy", (object[] args) => mockStrategyWithParams.Object.RunStrategy(args)).Execute();

        var sr = new StringReader("\n\n");
        var sw = new StringWriter();
        Console.SetIn(sr);
        Console.SetOut(sw);
        StartApp app = new StartApp(length);

        app.Execute();

        var writeResult = sw.ToString();
        Assert.Contains("Нажмите на клавишу для старта....", writeResult);
        Assert.Contains("Нажмите на клавишу для остановки...",writeResult);
        Assert.Contains("Все потоки застоплены и сервер тоже",writeResult);

    }
}