using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hwdtech;
using Hwdtech.Ioc;
using Xunit;

namespace SpaceBattle.Lib.Test;

public class BuildCodeStringAdapterTests
{
    [Fact]
    public void BuildString()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        Type type = typeof(IMovable);
        var properties = type.GetProperties().Select( p => new property(p.Name, p.PropertyType.Name, p.CanRead,p.CanWrite){
            }).ToArray()
            ;

        var builder = new CodeStringAdapterBuilder(className: "MovableAdapter", properties: properties);
        

var t = @"using System;

public class {{name }}
{
    private object obj;
    {{ for property in properties }}
    public {{ property.type }} {{ property.name }}
    {{if property.set}}
    {
        Hwdtech.IoC.Resolve<SpaceBattle.Lib.ICommand>(""{{property.name}}.Set"", obj, value).Execute();
    }
{{end}}  
    {{if property.get}}
               {
                        return Hwdtech.IoC.Resolve<{{property.type}}>(""{{property.name}}.Get"", obj);
                } 
{{ end }}

      
    {{ end }}
    public {{ name }}(object obj)
    {
        this.obj = obj;
    }
}";
    IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Template", (object []par)=> t).Execute();
    
    var result = builder.Build();
    System.Console.WriteLine(result);
    }
}