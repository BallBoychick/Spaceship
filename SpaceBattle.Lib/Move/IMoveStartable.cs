namespace SpaceBattle.Lib;

interface IMoveStartable
{
    IUObject Target
    {
        get;
    }
    IDictionary<string, object> Properties
    {
        get;
    }
}