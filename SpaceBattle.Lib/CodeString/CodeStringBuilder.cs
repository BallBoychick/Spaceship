namespace SpaceBattle.Lib;
using System.Reflection;
using Hwdtech;
using Scriban;

public class CodeStringAdapterBuilder : IBuilder
{

    private string ClassName;
    private  Property[] Properties;

    public CodeStringAdapterBuilder(string className,  Property[] properties)
    {
        this.ClassName = className;
        this.Properties = properties;

    }
    

    public string Build()
    {
        var templateText = IoC.Resolve<string>("Template");
        var template = Template.Parse(templateText);
        var result = template.Render(new {name = this.ClassName, properties = this.Properties});
        return result;
    }
}
