using Hwdtech.Ioc;
using Hwdtech;

using Moq;

namespace SpaceBattle.Lib.Test;

public class CheckCollisionCommandTests
{
    public CheckCollisionCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    }

    [Fact]
    public void SuccesfulDetectCollisionCommandTests()
    {
        var obj1 = new Mock<IUObject>();
        var obj2 = new Mock<IUObject>();

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();
        mockCommand.Setup(x => x.execute());

        var mockStrategyReturnsCommand = new Mock<IStrategy>();
        mockStrategyReturnsCommand.Setup(x => x.RunStrategy(obj1.Object, obj2.Object)).Returns(mockCommand.Object).Verifiable();

        var mockDict = new Mock<IDictionary<int, object>>();
        mockDict.Setup(x => x.Keys).Returns(new List<int>(){1});

        var mockStrategyReturnsTree = new Mock<IStrategy>();
        mockStrategyReturnsTree.Setup(x => x.RunStrategy()).Returns(mockDict.Object).Verifiable();

        var mockStrategyReturnsList = new Mock<IStrategy>();
        mockStrategyReturnsList.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(new List<int>()).Verifiable();

        var mockStrategyReturnsCorrentList = new Mock<IStrategy>();
        mockStrategyReturnsCorrentList.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(new List<int>()).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetSolutionTree", (object[] args) => mockStrategyReturnsTree.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.GetProperty", (object[] args) => mockStrategyReturnsList.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Collision", (object[] args) => mockStrategyReturnsCommand.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.TrainingDataToCollision", (object[] args) => mockStrategyReturnsCorrentList.Object.RunStrategy(args)).Execute();

        ICommand CheckCollision = new CheckCollisionCommand(obj1.Object, obj2.Object);

        CheckCollision.execute();

        mockStrategyReturnsCorrentList.VerifyAll();
        mockStrategyReturnsCommand.VerifyAll();
        mockStrategyReturnsList.VerifyAll();
        mockStrategyReturnsTree.VerifyAll();
    }
}
