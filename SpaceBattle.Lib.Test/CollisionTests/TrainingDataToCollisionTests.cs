using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace SpaceBattle.Lib.Test;

public class TrainingDataToColisionTests
{

    [Fact]
    public void SuccesfulTrainingDataToCollisionStrategyTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        List<int> object1 = new List<int>() { 1, 3, 5, 4 };
        List<int> object2 = new List<int>() { 1, 3, 5, 4 };

        IStrategy TrainingData = new TrainingDataToCollisionStrategy();

        Object.Equals(new List<int>() { 0, 0, 0, 0 }, TrainingData.RunStrategy(object1, object2));
    }
}
