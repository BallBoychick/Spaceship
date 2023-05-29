namespace SpaceBattle.Lib;
using System.Reflection;
using Hwdtech;
using Scriban;

public class CodeStringAdapterBuilder : IBuilder
{

    private string ClassName;
    private  List<property> Properties;

    public CodeStringAdapterBuilder(string className,  List<property> properties)
    {
        this.ClassName = className;
        this.Properties = properties;

    }

    public string Build()
    {
        var templateText = IoC.Resolve<string>("Template");
        var template = Template.Parse(templateText);
        var result = template.Render(new {ClassName = this.ClassName, Properties = this.Properties});
        return result;
    }
}
