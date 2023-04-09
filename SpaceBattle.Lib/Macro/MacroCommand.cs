namespace SpaceBattle.Lib;

public class MacroCommand : ICommand
{
    private IEnumerable<ICommand> cmd_list;

    public MacroCommand(IEnumerable<ICommand> cmd_list)
    {
        this.cmd_list = cmd_list;
    }
    public void Execute()
    {
        cmd_list.ToList().ForEach(cmd=> cmd.Execute());
    }
}
