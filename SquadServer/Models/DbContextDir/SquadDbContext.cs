using Microsoft.EntityFrameworkCore;
using SquadServer.Models.ModelsConfig;
namespace SquadServer.Models.DbContextDir;


public class SquadDbContext: DbContext
{
    public DbSet<UserModelEntity> Users {  get; set; }
    public SquadDbContext(DbContextOptions<SquadDbContext> options) : base (options)
    {
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserModelEntityConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SquadDb;Trusted_Connection=True";
        optionsBuilder.UseSqlServer(connectionString);
    }


}
