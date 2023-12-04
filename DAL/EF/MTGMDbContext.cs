using MTGM.BL.Domain;

namespace MTGM.DAL.EF;
using Microsoft.EntityFrameworkCore;

public class MtgmDbContext : DbContext
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<Deck> Decks { get; set; }
    public DbSet<Set> Sets { get; set; }
    public DbSet<DeckEntry> DeckEntries { get; set; }
    public DbSet<SetEntry> SetEntries { get; set; }

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
        modelBuilder.Entity<Deck>()
            .HasMany(d => d.Cards)
            .WithOne(de => de.Deck);

        modelBuilder.Entity<Card>()
            .HasMany(d => d.Decks)
            .WithOne(de => de.Card);
        
        modelBuilder.Entity<Set>()
            .HasMany(s => s.Cards)
            .WithOne(c => c.Set);
    }

}