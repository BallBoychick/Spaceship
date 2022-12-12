namespace SpaceBattle.Lib;

interface IMoveStopable
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