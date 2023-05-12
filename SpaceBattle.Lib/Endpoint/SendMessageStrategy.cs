using Hwdtech;
using SpaceBattle.Lib;

public class SendMessageStrategy : IStrategy
{
    public object RunStrategy(params object[] parameters)
    {
        var message = (IMessage)parameters[0];
        var threadID = message.GameID;
        var sender = IoC.Resolve<ISender>("Thread.QueueMessages" + threadID);
        var command = IoC.Resolve<SpaceBattle.Lib.ICommand>("CommandFromMessageStrategy", message);
        sender.Send(command);
        return true;
    }
}