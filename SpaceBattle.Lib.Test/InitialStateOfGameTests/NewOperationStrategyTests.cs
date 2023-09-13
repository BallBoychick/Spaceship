using System;
using Moq;
using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
public class NewOperationStrategyTests
{
    [Fact]
    public void SuccessfulGameNewOperationStrategyRun()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var listOfRules = new List<string>()
        {
            "Check"
        };

        var listOfRulesStrategy = new Mock<IStrategy>();
        listOfRulesStrategy.Setup(x => x.RunStrategy()).Returns(listOfRules);

        var checkCommand = new Mock<SpaceBattle.Lib.ICommand>();
        checkCommand.Setup(x => x.Execute()).Callback(() => {}).Verifiable();

        var checkCommandStrategy = new Mock<IStrategy>();

        checkCommandStrategy.Setup(x => x.RunStrategy(It.IsAny<IUObject[]>())).Returns(checkCommand.Object);


        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameRules.Rotate", (object[] args) => listOfRulesStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateGameCommand.Check", (object[] args) => checkCommandStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateGameMacroCommand", (object[] args) => new MacroCommand(new List<ICommand>(){checkCommand.Object})).Execute();



        var strategy = new RegsiterNewOperationStrategy();

        var actionCommand = (ICommand)strategy.RunStrategy();

        Assert.IsType<ActionCommand>(actionCommand);

        actionCommand.Execute();

        var obj = new Mock<IUObject>();

        var result = IoC.Resolve<ICommand>("NewGameOperation", obj.Object, "Rotate");

        Assert.IsType<MacroCommand>(result);

        result.Execute();

        checkCommand.VerifyAll();
    }
}
