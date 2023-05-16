using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;

public class GetUobjectFromObjectMapStrategyTests
{
    Dictionary<int, IUObject> objMap = new Dictionary<int, IUObject>(){
        {1, new Mock<IUObject>().Object}
    };

    public GetUobjectFromObjectMapStrategyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjects", (object[] args) => this.objMap).Execute();
    }

    [Fact]
    public void SuccessfulGetUObjectFromObjectMapStrategyRunStrategy()
    {
        var id = 1;

        var strategy = new GetUobjectFromMapStrategy();

        var q = strategy.RunStrategy(id);

        Assert.Equal(this.objMap[id], q);
    }

    [Fact]
    public void UnsuccessfulGetUObjectFromObjectMapStrategyRunStrategyBecauseCannotFindKeyUObjectMap()
    {
        var falseID = 2;

        var strategy = new GetUobjectFromMapStrategy();

        Assert.Throws<Exception>(() => strategy.RunStrategy(falseID));
    }
}
