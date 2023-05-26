using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;

public class RemoveUObjectFromMapCommandTests
{
        Dictionary<int, IUObject> objMap = new Dictionary<int, IUObject>(){
        {1, new Mock<IUObject>().Object}
    };

    public RemoveUObjectFromMapCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjects", (object[] args) => this.objMap).Execute();
    }

    [Fact]
    public void Execute_RemovesUObjectFromDictionary()
    {
        var id = 1;
        Assert.Equal(1, objMap.Count);
        var comm = new RemoveUObjectFromMapCommand(id);

        comm.Execute();

        Assert.Equal(0, objMap.Count);
    }

    [Fact]
    public void UnsuccessfulGetUObjectFromObjectMapStrategyRunStrategyBecauseCannotFindKeyUObjectMap()
    {
        var falseID = 2;

        Assert.Equal(1, objMap.Count);
        var comm = new RemoveUObjectFromMapCommand(falseID);

        Assert.Throws<Exception>(() => comm.Execute());
    }
}
