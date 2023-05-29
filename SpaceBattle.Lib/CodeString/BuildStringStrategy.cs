
namespace SpaceBattle.Lib;

public class property
{
    public string name;
    public string type;
    public bool get;
    bool set;
    public property(string name, string type, bool get, bool set){
        this.name = name;
        this.type = type;
        this.get = get;
        this.set = set;
    }
}