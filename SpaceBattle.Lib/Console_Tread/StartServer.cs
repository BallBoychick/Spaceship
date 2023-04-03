namespace SpaceBattle.Lib;
using Hwdtech;

public class StartServer : ICommand
{
    private int length;
    public StartServer(int length)
    {
        this.length = length;
    }

    public void execute()
    {
        var numbers = new int[] {length};
        foreach (int len in numbers)
        {
            IoC.Resolve<ICommand>("CreateAndStartThreadStrategy", len).execute();
        }
        Console.WriteLine("Start" + length + " threads");
    }
}