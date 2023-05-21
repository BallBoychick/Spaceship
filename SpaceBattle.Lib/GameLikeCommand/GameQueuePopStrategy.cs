namespace SpaceBattle.Lib;

public class GameQueuePopStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var id = (int)args[0];
        var cmd = (ICommand)args[1];

        return new GameQueuePopCommand(id, cmd);
    }
}
