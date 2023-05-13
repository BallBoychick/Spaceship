using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
public class InterpreterCommandTests
{
    [Fact]
        public void TestExecute()
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
            var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
            mockCommand.Setup(x => x.Execute());

            var mockStrategyReturnsCommand = new Mock<IStrategy>();
            mockStrategyReturnsCommand.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(mockCommand.Object);

            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.CreateCommand", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();
        }

    [Fact]
    public void InterpreterCommand_CanBeConstructed()
    {
        var commandMock = new Mock<ICommand>();
        var message = Mock.Of<IMessage>();
        var InterpreterCommand = new InterpreterCommand(message);

        Assert.NotNull(InterpreterCommand);
    }
        
}
