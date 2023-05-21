using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class InterpreterCommand : ICommand
{
    private IMessage message;

    public InterpreterCommand(IMessage message)
    {
        this.message = message;
    }

    public void Execute()
    {
        var cmd = IoC.Resolve<ICommand>("Game.CreateCommand", message);

        var id = message.GameID;

        IoC.Resolve<ICommand>("Game.Queue.Push", id, cmd).Execute();
    }
}
