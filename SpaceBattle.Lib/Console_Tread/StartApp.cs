namespace SpaceBattle.Lib;
using Hwdtech;

public class StartApp
{
    private int numberthread;

    public StartApp(int numberthread)
    {
        this.numberthread = numberthread;
    }
    public void Execute()
    {
        Console.WriteLine("Нажмите на клавишу для старта....");
        Console.Read();

        IoC.Resolve<ICommand>("StartServerStrategy", numberthread).Execute();

        Console.WriteLine("Нажмите на клавишу для остановки...");
        Console.Read();

        IoC.Resolve<ICommand>("StopServerStrategy").Execute();
        Console.WriteLine("Все потоки застоплены и сервер тоже");
        //мб можно стратегию замутить, но **** ааааа
    }
}
