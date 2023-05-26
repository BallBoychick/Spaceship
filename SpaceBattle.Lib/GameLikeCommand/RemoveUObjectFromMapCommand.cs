using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class RemoveUObjectFromMapCommand : ICommand
{
    private int gameId;

    public RemoveUObjectFromMapCommand(int gameId)
    {
        this.gameId = gameId;
    }
    public void Execute() 
    { 
        var uObjectDictionary = IoC.Resolve<IDictionary<int, IUObject>>("GetUObjects"); 
        if (!uObjectDictionary.TryGetValue(gameId, out IUObject? obj))  
            throw new Exception("IUObject with specified Id not found in dictionary."); 

        uObjectDictionary.Remove(gameId);
    } 
}
