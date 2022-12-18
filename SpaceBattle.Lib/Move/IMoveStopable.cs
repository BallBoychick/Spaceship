namespace SpaceBattle.Lib;

public interface IMoveStopable
{
    IEnumerable<string> Properties
    {
        get;
    }
    IUObject Target
    {
        get;
    }
}