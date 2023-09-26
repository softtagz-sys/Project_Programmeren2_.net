using MagicTheGatheringManagement.Domain;

namespace MagicTheGatheringManagement;

public class Program
{
    public static void Main()
    {
        var consoleUi = new ConsoleUi();
        consoleUi.Run();
    }
}