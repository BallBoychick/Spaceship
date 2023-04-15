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
        IoC.Resolve<ICommand>("InputAppStrategy", "Нажмите клавишу для старта...").Execute();
        IoC.Resolve<ICommand>("StartAppStrategy", numberthread).Execute();

        IoC.Resolve<ICommand>("InputAppStrategy", "Нажмите на клавишу для остановки...").Execute();

        IoC.Resolve<ICommand>("StopServerStrategy").Execute();
    }
}
