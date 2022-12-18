using Hwdtech;

namespace SpaceBattle.Lib;

public class MovementStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        IUObject obj = (IUObject)args[0];
        IEnumerable<ICommand> list_command = IoC.Resolve<IEnumerable<ICommand>>("Game.CreateMove", obj);

        ICommand macro_сommand = IoC.Resolve<ICommand>("Game.Command.Macro", list_command);

        ICommand inject_command = IoC.Resolve<ICommand>("Game.Command.Inject", macro_сommand);

        ICommand repeat_сommand = IoC.Resolve<ICommand>("Game.Command.Repeat", inject_command);
        list_command.Append(repeat_сommand);
        
        return inject_command;
        //повторяющуюся рипитнутую команду
        //StartMove
        //Macro Command
        //Injecting Command
        //Repeat Command
        //return vishe
        //Why is it in lab2????
    }

}
//MovementStrategy.Strategy(params object[])" не может реализовать "IStrategy.Strategy(params object[])", потому что не
//имеет соответствующего возвращаемого типа "object".

//error CS0161: "MovementStrategy.Strategy(params object[])": не все пути к коду
//возвращают значение. [D:\ООАиП\SpaceBattle.Lib\SpaceBattle.Lib.csproj]