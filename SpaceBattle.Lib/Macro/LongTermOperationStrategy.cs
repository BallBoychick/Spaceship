using Hwdtech;
namespace SpaceBattle.Lib;

public class LongTermOperationStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        string name = (string)args[0];
        IUObject uobj = (IUObject)args[1];

        var macro = IoC.Resolve<ICommand>("CreateMacroStrategy", name, uobj);

        ICommand repeatCommand = IoC.Resolve<ICommand>("Game.Command.Repeat", macro);
        ICommand injectCommand = IoC.Resolve<ICommand>("Game.Command.Inject", repeatCommand);

        return injectCommand;
    }
}