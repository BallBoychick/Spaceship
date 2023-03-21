namespace SpaceBattle.Lib;
using Hwdtech;

public class ChangeBehaviorThreadStrategy : IStrategy
{

    public object RunStrategy(params object[] parameters)
    {
        String idThread = (string)parameters[0];
        Action action = (parameters.Count() == 2) ? (Action)parameters[1] : () => { };
        
        var thread = IoC.Resolve<ServerThread>("Threads."+ idThread);
        Action newAction = () =>
        {
            thread.HandleCommand();
            action();
        };
        thread.ChangeBehaviour(newAction);
        return true;
    }
}
