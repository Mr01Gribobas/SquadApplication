namespace SquadServer.Models.DbContextDir;


public class SquadDbContext: DbContext
{
    public DbSet<UserModelEntity> Players {  get; set; }
    public DbSet<PolygonEntity> Polygons {  get; set; }
    public DbSet<EquipmentEntity> Equipments {  get; set; }
    public DbSet<ReantalEntity> Reantils { get; set; }
    public DbSet<EventModelEntity> Events { get; set; }
    public DbSet<HisoryEventsModelEntity> HistoryEvents { get; set; }
    public SquadDbContext(DbContextOptions<SquadDbContext> options) : base (options)
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserModelEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PolygonEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EventModelEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ReantalEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new HisoryEventsModelEntityConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=SquadDb;Trusted_Connection=True";
        optionsBuilder.UseSqlServer(connectionString);
    }


}
