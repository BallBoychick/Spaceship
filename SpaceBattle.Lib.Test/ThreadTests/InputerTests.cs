using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
using System.Collections.Concurrent;

public class InputerTests
{
    [Fact]
    public void Correct_Inputer_Execute()
    {
        var str = "Hello, World";
        var cmd = new InputAppCommand(str);
        var sw = new StringWriter();
        Console.SetOut(sw);

        cmd.Execute();

        var writeResult = sw.ToString();
        Assert.Contains(str, writeResult);
    }

    [Fact]

    public void Correct_Inputer_Strategy()
    {
        var strategy = new InputAppStrategy();
        var str = "Hello, Ivan Vladimirovich";
        var cmd = strategy.RunStrategy(str);

        Assert.NotNull(cmd);
    }
}