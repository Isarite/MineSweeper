using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MineServer.Resources;

namespace MineServer.Models
{
    public class MineSweeperContext : IdentityDbContext<Player>
    {
        public DbSet<Cell> Cells { get; set; }
        public DbSet<PlayerStrategy> Strategies { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Map> Maps { get; set; }
        
        //public DbSet<MapMemento> Mementoes { get; set; }

        public MineSweeperContext(DbContextOptions<MineSweeperContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Unknown>().HasBaseType<Cell>();
            modelBuilder.Entity<Tnt>().HasBaseType<Cell>();
            modelBuilder.Entity<ExplodedTnt>().HasBaseType<Cell>();
            modelBuilder.Entity<WrongTnt>().HasBaseType<Cell>();
            modelBuilder.Entity<Revealed>().HasBaseType<Cell>();


            modelBuilder.Entity<SetMine>().HasBaseType<PlayerStrategy>();
            modelBuilder.Entity<UnsetMine>().HasBaseType<PlayerStrategy>();
            modelBuilder.Entity<RevealCell>().HasBaseType<PlayerStrategy>();
            modelBuilder.Entity<MarkCell>().HasBaseType<PlayerStrategy>();


            modelBuilder.Entity<Player>().HasMany(u => u.strategies).WithOne(s => s.player);

            modelBuilder.Entity<Game>().HasMany(g => g.players).WithOne(p => p.currentGame);


            modelBuilder.Entity<Game>().HasOne(d => d.GameMap);
            modelBuilder.Entity<Game>().HasOne(d => d.Memento);
            modelBuilder.Entity<Game>().Property(e => e.Status)
                .HasConversion(x => (int) x, x => (GameStatus) x);

            modelBuilder.Entity<Map>().HasMany(d => d.Cells).WithOne(c => c.map).IsRequired();

            //modelBuilder.Entity<MapMemento>().HasMany(d => d.Cells).WithOne(c => c.Memento).IsRequired();


            modelBuilder.Entity<Cell>().HasKey(d => d.Id);
            modelBuilder.Entity<PlayerStrategy>().HasKey(d => d.Id);
            modelBuilder.Entity<Game>().HasKey(d => d.Id);
            modelBuilder.Entity<Map>().HasKey(d => d.Id);
            modelBuilder.Entity<MapMemento>().HasKey(d => d.Id);


            modelBuilder.Entity<Cell>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<PlayerStrategy>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Game>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Map>().Property(p => p.Id).ValueGeneratedOnAdd();


            //modelBuilder.Entity<Map>().HasKey(d => d.id);

            //modelBuilder.Entity<DiaryPage>().HasKey(p => p.id);
            //modelBuilder.Entity<DiaryPage>().HasMany(p => p.markings).WithOne();

            //modelBuilder.Entity<Marking>().HasKey(m => m.id);

        }
    }

}
