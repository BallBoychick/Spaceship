namespace SpaceBattle.Lib;
using Hwdtech;

class Program
{
    static void Main(string[] args)
    {
        int numberthread = int.Parse(args[0]);
        var startapp = new StartApp(numberthread);
        startapp.Execute();
        Console.WriteLine("EZ");
    }
}