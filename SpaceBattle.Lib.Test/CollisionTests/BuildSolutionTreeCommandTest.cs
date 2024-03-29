using Hwdtech.Ioc;
using Hwdtech;

using Moq;

namespace SpaceBattle.Lib.Test;

public class SolutionTreeTests
{

    public SolutionTreeTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockStrategyReturnsDict = new Mock<IStrategy>();
        mockStrategyReturnsDict.Setup(x => x.RunStrategy()).Returns(new Dictionary<int, object>());

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetSolutionTree", (object[] args) => mockStrategyReturnsDict.Object.RunStrategy(args)).Execute();
    }

    [Fact]
    public void SuccesfulBuildSolutionTree()
    {
        var path = "../../../file.txt";

        var buildCommand = new BuildSolutionTreeCommand(path);

        buildCommand.Execute();

        var tree = IoC.Resolve<IDictionary<int, object>>("Game.GetSolutionTree");

        Assert.True(tree.ContainsKey(1));
        Assert.True(tree.ContainsKey(4));
        Assert.True(tree.ContainsKey(2));
        Assert.True(tree.ContainsKey(9));

        Assert.True(((IDictionary<int, object>)tree[1]).ContainsKey(3));
        Assert.True(((IDictionary<int, object>)tree[1]).ContainsKey(2));

        Assert.False(((IDictionary<int, object>)((IDictionary<int, object>)tree[1])[2]).ContainsKey(9));
    }
}
