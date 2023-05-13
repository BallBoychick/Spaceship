using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class GameSetPropertiesCommand : ICommand
{
    private IUObject uobj;

    private string key;

    private object value;

    public GameSetPropertiesCommand(IUObject uobj, string key, object value)
    {
        this.uobj = uobj;
        this.key = key;
        this.value = value;
    }

    public void Execute()
    {
        uobj.setProperty(this.key, this.value);
    }
}
