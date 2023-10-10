using System.Net.Mime;
using MagicTheGatheringManagement.Domain;
using MTGM.BL;
using MTGM.DAL;

namespace MagicTheGatheringManagement;

public class Program
{
    static void Main(string[] args)
    {
        IRepository repository = new InMemoryRepository();
        IManager manager = new Manager(repository);

        InMemoryRepository.Seed();
        
        var consoleUi = new ConsoleUi(manager);
        if (consoleUi == null) throw new ArgumentNullException(nameof(consoleUi));
        consoleUi.Run();
    }
    
    
}