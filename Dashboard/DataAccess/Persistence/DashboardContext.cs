using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Persistence {
  public class DashboardContext : DbContext {
    public DashboardContext() {
    }

    public DashboardContext(DbContextOptions<DashboardContext> options) : base(options) {
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Configs> Configs { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Dashboard> Dashboards { get; set; }
  }
}