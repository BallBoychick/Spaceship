using Hwdtech;
using Hwdtech.Ioc;
using System;
namespace SpaceBattle.Lib;

public class RegsiterNewOperationStrategy : IStrategy
{
    IStrategy newOperationStrategy = new NewOperationStrategy();
    public object RunStrategy(params object[] args)
    {
        return new ActionCommand(() => {IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "NewGameOperation", (object[] args) => 
        this.newOperationStrategy.RunStrategy(args)).Execute();
        });
    }
}
