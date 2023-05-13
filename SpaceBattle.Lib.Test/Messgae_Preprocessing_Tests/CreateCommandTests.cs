using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
public class CreateCommandTests
{
    // [Fact]
    // public void TestExecute()
    // {
    //     new InitScopeBasedIoCImplementationCommand().Execute();
    //     IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    //     var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
    //     mockCommand.Setup(x => x.Execute());

    //     var mockStrategyReturnsCommand = new Mock<IStrategy>();
    //     mockStrategyReturnsCommand.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(mockCommand.Object);

    //     // IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjFromMap", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();

    //     var message = new Mock<IMessage>();
    //     // createCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<IMessage>())).Returns(message).Verifiable();

    //     message.Setup(x => x.GameID).Throws<Exception>();
        
    //     var interpretingCommand = new CreateCommandStrategy();

    //     Assert.Throws<Exception>(() => interpretingCommand.RunStrategy(message.Object));
    // }
}
