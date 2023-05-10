namespace SpaceBattle.Lib;
using Hwdtech;

public class StartServer : ICommand
{
    private int length;
    public StartServer(int length)
    {
        this.length = length;
    }

    public void Execute()
    {
        for (int i = 0; i < length; i++)
        {
            IoC.Resolve<ICommand>("CreateAndStartThreadStrategy", i).Execute();
        }
    }
}
