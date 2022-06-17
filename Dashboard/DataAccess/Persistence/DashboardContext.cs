using System.Data;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Persistence
{
    public class DashboardContext : DbContext
    {
        public IDbConnection Connection => Database.GetDbConnection();

        public DashboardContext()
        {
        }

        public DashboardContext(DbContextOptions<DashboardContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Dashboard>().HasKey(entity => new { entity.Id });
            builder.Entity<Account>().HasKey(entity => new { entity.UserId });
            builder.Entity<Configs>().HasKey(entity => new { entity.Id });
            builder.Entity<Contact>().HasKey(entity => new { entity.Id });
            builder.Entity<Core.Entities.Task>().HasKey(entity => new { entity.Id });
            builder.Entity<Widget>().HasKey(entity => new { entity.WidgetId });
            builder.HasDefaultSchema("public");
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=DashboardDB;Username=postgres;Password=123456");

        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Configs> Configs { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;
        public DbSet<Dashboard> Dashboards { get; set; } = null!;
        public DbSet<Core.Entities.Task> Tasks { get; set; } = null!;
        public DbSet<Widget> Widgets { get; set; } = null!;
    }
}
