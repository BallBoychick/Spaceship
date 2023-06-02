namespace SpaceBattle.Lib;
using System.Reflection;
using Hwdtech;
using Scriban;

public class CodeStringAdapterBuilder : IBuilder
{

    private string ClassName;
    private  List<object> Members;

    public CodeStringAdapterBuilder(string className)
    {
        this.ClassName = className;
        this.Members = new List<object>();

    }
    public CodeStringAdapterBuilder AddMember(object property)
    {
        this.Members.Add(property);
        return this;
    }
    

    public string Build()
    {
        var templateText = IoC.Resolve<string>("Template");
        var template = Template.Parse(templateText);
        var result = template.Render(new {name = this.ClassName, properties = this.Members.ToArray()});
        return result;
    }
}
