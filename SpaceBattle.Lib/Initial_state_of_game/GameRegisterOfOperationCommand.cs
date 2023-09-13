using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class GameRegisterOfOperationCommand : ICommand
{
    private int gameID;
    public GameRegisterOfOperationCommand(int gameID)
    {
        this.gameID = gameID;
    }

    public void Execute()
    {
        var cmd = IoC.Resolve<ICommand>("GamesThreadRgisterOperationStrategy");
        IoC.Resolve<ICommand>("GameQueuePushStrategy", gameID, cmd).Execute();
    }
}
