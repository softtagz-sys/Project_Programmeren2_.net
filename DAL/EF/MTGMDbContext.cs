using MTGM.BL.Domain;

namespace MTGM.DAL.EF;
using Microsoft.EntityFrameworkCore;

public class MtgmDbContext : DbContext
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<Deck> Decks { get; set; }
    public DbSet<Set> Sets { get; set; }
    
    public MtgmDbContext(DbContextOptions<MtgmDbContext> options) : base(options)
    {
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=MTGM.db");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
    
    public bool CreateDatabase(bool wipeDatabase = false)
    {
        if (wipeDatabase)
        {
            Database.EnsureDeleted();
        }

        return Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //TODO
    }

}