using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
public class CreateCommandTests
{
    [Fact]
    public void SuccessfulCreateCommandExecute()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        
        var obj = new Mock<IUObject>();

        var getUObjFromMapStrategy = new Mock<IStrategy>();

        getUObjFromMapStrategy.Setup(s => s.RunStrategy(It.IsAny<int>())).Returns(obj.Object).Verifiable();

        var setPropCom = new Mock<ICommand>();
        setPropCom.Setup(c => c.Execute()).Callback(() => {});

        var setPropComStrategy = new Mock<IStrategy>();
        setPropComStrategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(setPropCom.Object).Verifiable();

        var commandMock = new Mock<ICommand>();

        var mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsCommand.Setup(x => x.RunStrategy(It.IsAny<IUObject>())).Returns(commandMock.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjFromMap", (object[] args) => getUObjFromMapStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameSetProperties", (object[] args) => setPropComStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Move", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.GameItemID).Returns(1).Verifiable();
        message.Setup(m => m.OrderType).Returns("Move").Verifiable();
        message.Setup(m => m.Properties).Returns(new Dictionary<string, object>()
        {
            {"Dict", 1}
        }).Verifiable();

        var strategy = new CreateCommandStrategy();

        var cmd = strategy.RunStrategy(message.Object);
        message.Verify();
        setPropComStrategy.VerifyAll();
        getUObjFromMapStrategy.VerifyAll();
        mockStrategyReturnsCommand.VerifyAll();

        Assert.NotNull(cmd);

    }
    [Fact]
    public void FailCreateCommandThrowExceptionOrderType()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        
        var obj = new Mock<IUObject>();

        var getUObjFromMapStrategy = new Mock<IStrategy>();
        getUObjFromMapStrategy.Setup(s => s.RunStrategy(It.IsAny<int>())).Returns(obj.Object).Verifiable();

        var setPropCom = new Mock<ICommand>();
        setPropCom.Setup(c => c.Execute()).Callback(() => {});

        var setPropComStrategy = new Mock<IStrategy>();
        setPropComStrategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(setPropCom.Object).Verifiable();

        var commandMock = new Mock<ICommand>();

        var mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsCommand.Setup(x => x.RunStrategy(It.IsAny<IUObject>())).Returns(commandMock.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjFromMap", (object[] args) => getUObjFromMapStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameSetProperties", (object[] args) => setPropComStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Move", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.OrderType).Throws<Exception>();

        var strategy = new CreateCommandStrategy();

        Assert.Throws<Exception>(() => strategy.RunStrategy(message.Object));
    }

    [Fact]
    public void FailCreateCommandThrowExceptionGameItemID()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        
        var obj = new Mock<IUObject>();

        var getUObjFromMapStrategy = new Mock<IStrategy>();
        getUObjFromMapStrategy.Setup(s => s.RunStrategy(It.IsAny<int>())).Returns(obj.Object).Verifiable();

        var setPropCom = new Mock<ICommand>();
        setPropCom.Setup(c => c.Execute()).Callback(() => {});

        var setPropComStrategy = new Mock<IStrategy>();
        setPropComStrategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(setPropCom.Object).Verifiable();

        var commandMock = new Mock<ICommand>();

        var mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsCommand.Setup(x => x.RunStrategy(It.IsAny<IUObject>())).Returns(commandMock.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjFromMap", (object[] args) => getUObjFromMapStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameSetProperties", (object[] args) => setPropComStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Move", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.GameItemID).Throws<Exception>();

        var strategy = new CreateCommandStrategy();

        Assert.Throws<Exception>(() => strategy.RunStrategy(message.Object));
    }

    [Fact]
    public void FailCreateCommandThrowExceptionProperties()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        
        var obj = new Mock<IUObject>();

        var getUObjFromMapStrategy = new Mock<IStrategy>();
        getUObjFromMapStrategy.Setup(s => s.RunStrategy(It.IsAny<int>())).Returns(obj.Object).Verifiable();

        var setPropCom = new Mock<ICommand>();
        setPropCom.Setup(c => c.Execute()).Callback(() => {});

        var setPropComStrategy = new Mock<IStrategy>();
        setPropComStrategy.Setup(c => c.RunStrategy(It.IsAny<object[]>())).Returns(setPropCom.Object).Verifiable();

        var commandMock = new Mock<ICommand>();

        var mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsCommand.Setup(x => x.RunStrategy(It.IsAny<IUObject>())).Returns(commandMock.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjFromMap", (object[] args) => getUObjFromMapStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameSetProperties", (object[] args) => setPropComStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Move", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.Properties).Throws<Exception>();

        var strategy = new CreateCommandStrategy();

        Assert.Throws<Exception>(() => strategy.RunStrategy(message.Object));
    }
}
