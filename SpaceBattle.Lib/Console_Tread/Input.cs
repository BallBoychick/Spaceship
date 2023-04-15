namespace SpaceBattle.Lib;
using Hwdtech;

public class InputAppCommand : ICommand
{
    private string? text;

    public InputAppCommand(string text)
    {
        this.text = text;
    }
    public void Execute()
    {
        Console.WriteLine(text);
    }
}
