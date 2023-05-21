using Hwdtech;
using System;
namespace SpaceBattle.Lib;

public class RemoveUObjectFromMapStrategy : IStrategy 
{ 
    public object RunStrategy(params object[] args) 
    { 
        var id = (int)args[0]; 
 
        var uObjectDictionary = IoC.Resolve<IDictionary<int, IUObject>>("GetUObjects"); 
        if (!uObjectDictionary.TryGetValue(id, out IUObject? obj))  
            throw new Exception("IUObject with specified Id not found in dictionary."); 

        uObjectDictionary.Remove(id);
        return obj; 
    } 
}