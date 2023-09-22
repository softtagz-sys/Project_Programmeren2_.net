using MagicTheGatheringManagement.Domain;

namespace MagicTheGatheringManagement;

public class Program
{
    public static void Main(string[] args)
    {
        var consoleUi = new ConsoleUi();
        consoleUi.Run();
    }
}