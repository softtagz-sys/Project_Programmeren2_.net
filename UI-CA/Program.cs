using System.Net.Mime;
using System.Threading.Tasks.Dataflow;
using MagicTheGatheringManagement.Domain;
using Microsoft.EntityFrameworkCore;
using MTGM.BL;
using MTGM.DAL;
using MTGM.DAL.EF;

namespace MagicTheGatheringManagement;

public class Program
{
    static void Main(string[] args)
    {
        var dbOptionsBuilder = new DbContextOptionsBuilder<MtgmDbContext>();
        dbOptionsBuilder.UseSqlite("Data Source=MTGM.db");
        MtgmDbContext ctx = new MtgmDbContext(dbOptionsBuilder.Options);
        IRepository repository = new Repository(ctx);
        //IRepository repository = new InMemoryRepository();
        IManager manager = new Manager(repository);
        ConsoleUi consoleUi = new ConsoleUi(manager);

        //InMemoryRepository.Seed();
        if (ctx.CreateDatabase())
        {
            DataSeeder.Seed(ctx);
        }
        
        if (consoleUi == null) throw new ArgumentNullException(nameof(consoleUi));
        consoleUi.Run();
    }
    
    
}