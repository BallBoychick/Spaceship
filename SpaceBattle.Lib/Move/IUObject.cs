namespace SpaceBattle.Lib;

public interface IUObject
{
    void setProperty(string key, object value);
    object getProperty(string key);
}

// var value = IoC.Resolve<object>("UObject.getProperty", object, key);
// var cmd = IoC.Resolve<object>("UObject.setProperty", object, key, value);
// cmd.execute();