namespace SpaceBattle.Lib;

public class StartAppStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        return new StartServer((int)args[0]);
    }
}