using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;

public class RemoveUobjectFromObjectMapStrategyTests
{
    Dictionary<int, IUObject> objMap = new Dictionary<int, IUObject>(){
        {1, new Mock<IUObject>().Object}
    };

    public RemoveUobjectFromObjectMapStrategyTests()
    {
        var delStrategy = new Mock<IStrategy>();

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjects", (object[] args) => this.objMap).Execute();
        // IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjects", (object[] args) => { objMap.Remove(1); return null; }).Execute();
    }

    [Fact]
    public void RunStrategy_ShouldRemoveIUObjectFromDictionary()
    {
        var id = 1;

        var strategy = new RemoveUObjectFromMapStrategy();

        var q = strategy.RunStrategy(id);

        Assert.False(objMap.TryGetValue(id, out IUObject? obj));
    }

    [Fact]
    public void UnsuccessfulGetUObjectFromObjectMapStrategyRunStrategyBecauseCannotFindKeyUObjectMap()
    {
        var falseID = 2;

        var strategy = new RemoveUObjectFromMapStrategy();

        Assert.Throws<Exception>(() => strategy.RunStrategy(falseID));
    }
}
