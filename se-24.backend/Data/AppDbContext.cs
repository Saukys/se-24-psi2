using Microsoft.EntityFrameworkCore;
using se_24.backend.Models;
using se_24.shared.src.Games.FinderGame;
using se_24.shared.src.Games.ReadingGame;
using se_24.shared.src.Shared;

namespace se_24.backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Authentication entities
        public DbSet<User> Users { get; set; }

        // Game entities
        public DbSet<ReadingLevel> ReadingLevels { get; set; }
        public DbSet<ReadingQuestion> ReadingQuestions { get; set; }
        public DbSet<Level> FinderLevels { get; set; }
        public DbSet<GameObject> FinderLevelGameObjects { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User configuration
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Game configurations
            modelBuilder.Entity<ReadingLevel>().HasKey(rl => rl.Id);
            modelBuilder.Entity<ReadingQuestion>().HasKey(rq => rq.Id);
            modelBuilder.Entity<GameObject>().HasKey(go => go.Id);
            modelBuilder.Entity<Level>().HasKey(l => l.Id);
            modelBuilder.Entity<Score>().HasKey(s => s.Id);
            modelBuilder.Entity<GameObject>()
                        .OwnsOne(g => g.Position);
        }
    }
} 