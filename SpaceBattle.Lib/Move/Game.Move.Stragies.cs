using Hwdtech;

namespace SpaceBattle.Lib;

public class MovementStrategy : IStrategy
{
    public object Strategy(params object[] args)
    {
        IUObject obj = (IUObject)args[0];
        return obj;
        //повторяющуюся рипитнутую команду
    }

}
//MovementStrategy.Strategy(params object[])" не может реализовать "IStrategy.Strategy(params object[])", потому что не
//имеет соответствующего возвращаемого типа "object".

//error CS0161: "MovementStrategy.Strategy(params object[])": не все пути к коду
//возвращают значение. [D:\ООАиП\SpaceBattle.Lib\SpaceBattle.Lib.csproj]