using Hwdtech;

namespace SpaceBattle.Lib;

public class TrainingDataToCollisionStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        List<int> list = new List<int>();

        var prop1 = (List<int>)args[0];
        var prop2 = (List<int>)args[1];
        var numbersAndWords = prop1.Zip(prop2, (first, second) => first + " " + second);
        foreach (var item in numbersAndWords)
        {
            list.Add(item[0] - item[2]);
        }
        return list;
    }
}