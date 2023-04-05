namespace SpaceBattle.Lib;
using Hwdtech;

public class HardStopThreadStrategy : IStrategy
{

    public object RunStrategy(params object[] parameters)
    {
        String idThread = (string)parameters[0];
        Action action = (parameters.Count() == 2) ? (Action)parameters[1] : () => { };
       
        var thread = IoC.Resolve<ServerThread>("Threads."+ idThread);
        var stopThreadCommand = new StopThreadCommand(thread);
        var changeBehaviorStopThreadCommand = new ChangeBehaviorStopThreadCommand(() => {thread.StopThread();
            action();},stopThreadCommand);
        
        changeBehaviorStopThreadCommand.execute();
        IoC.Resolve<Lib.ICommand>("Send Command", idThread, stopThreadCommand).execute();
        return true;
    }
}
