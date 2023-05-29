
namespace SpaceBattle.Lib;

public class Property
{
    public string name;
    public string type;
    public bool get;
    public bool set;
    public Property(string name, string type, bool get, bool set){
        this.name = name;
        this.type = type;
        this.get = get;
        this.set = set;
    }
}
