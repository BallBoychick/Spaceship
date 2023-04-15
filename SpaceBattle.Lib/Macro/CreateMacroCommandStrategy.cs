using Hwdtech;

namespace SpaceBattle.Lib;

public class CreateMacroStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var name = (string)args[0];
        var obj = (IUObject)args[1];

        var list_string = IoC.Resolve<IEnumerable<string>>("SetupStringOperation." + name);
        var list_cmds = list_string.Select(str => IoC.Resolve<ICommand>(str, obj));
        var macro_cmd = IoC.Resolve<ICommand>("CreateMacroCommand", list_cmds);

        return macro_cmd;
    }
}
