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
        var numbers = new int[] {length};
        foreach (int len in numbers)
        {
            IoC.Resolve<ICommand>("CreateAndStartThreadStrategy", len).Execute();
        }
        Console.WriteLine("Start" + length + " threads");
    }
}