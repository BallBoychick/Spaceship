namespace SpaceBattle.Lib;
using Hwdtech;

public class InputAppStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        return new InputAppCommand((string)args[0]);
    }
}
