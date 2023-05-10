namespace SpaceBattle.Lib;
using Hwdtech;

public interface ISender
{
    public void Send(ICommand message);
}
