using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
public class CreateNewGameStrategyTests
{
    [Fact]
    public void SuccessfulGameCreateStrategyTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(x => x.Execute());

        var mockStrategyReturnsCommand = new Mock<IStrategy>();

        mockStrategyReturnsCommand.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(mockCommand.Object);

        var mockStrategyReturnsDictionary = new Mock<IStrategy>();
        mockStrategyReturnsDictionary.Setup(x => x.RunStrategy()).Returns(new Dictionary<int, SpaceBattle.Lib.ICommand> { { 1, mockCommand.Object } });

        var mockStrategyReturnsScopeDictionary = new Mock<IStrategy>();
        mockStrategyReturnsScopeDictionary.Setup(x => x.RunStrategy()).Returns(new Dictionary<int, object> { { 0, IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))) } });

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameScope", (object[] args) => mockStrategyReturnsScopeDictionary.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameMap", (object[] args) => mockStrategyReturnsDictionary.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "MacroGameCommand", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "InjectGameCommand", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "RepeatGameCommand", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameCommand", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();

        var Game = new CreateNewGameStrategy();
        var res = Game.RunStrategy(0);

        var map = IoC.Resolve<IDictionary<int, Lib.ICommand>>("GameMap");
        Assert.Equal(map[0], mockCommand.Object);

    }
}
