using Hwdtech;

namespace SpaceBattle.Lib;

public class BuildSolutionTreeCommand : ICommand
{
    private string file;
    public BuildSolutionTreeCommand(string file)
    {
        this.file = file;
    }
    public void execute()
    {
        var parametrs = File.ReadAllLines(file).ToList().Select(line => line.Split(" ").Select(int.Parse).ToList()).ToList();

        var tree = IoC.Resolve<IDictionary<int, object>>("Game.GetSolutionTree");

        parametrs.ForEach(list =>
        {
            var temp = tree;
            list.ForEach(num =>
            {
                temp.TryAdd(num, new Dictionary<int, object>());
                temp = (Dictionary<int, object>)temp[num];
            });
        });
    }
}
