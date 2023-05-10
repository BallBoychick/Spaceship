namespace SpaceBattle.Lib;
using Hwdtech;

public class HandlerWriterCommand : ICommand
{
    private string message;
    private string path;
    public HandlerWriterCommand(string path, string message)
    {
        this.message = message;
        this.path = path;
    }

    public void Execute()
    {
        string createText = message + Environment.NewLine;
        File.AppendAllText(path, createText);
    }
}
