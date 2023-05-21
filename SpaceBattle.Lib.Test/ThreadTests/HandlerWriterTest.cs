using Hwdtech;
using Hwdtech.Ioc;
namespace SpaceBattle.Lib.Test;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class HandlerWriterTests
{

    [Fact]
    public void Correct_Writer_ExecuteAsync()
    {
        var path = "../../../Errors.txt";
        var cmd = new HandlerWriterCommand(path, "Error3");
        cmd.Execute();

        var str = File.ReadLines(path).ToList().Last();


        Assert.Equal("Error3", str);


        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
