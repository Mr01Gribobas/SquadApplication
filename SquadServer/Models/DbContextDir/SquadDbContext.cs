namespace SquadServer.Models.DbContextDir;

public class SquadDbContext: DbContext
{
    public DbSet<UserModelEntity> Players {  get; set; }
    public DbSet<PolygonEntity> Polygons {  get; set; }
    public DbSet<EquipmentEntity> Equipments {  get; set; }
    public DbSet<ReantalEntity> Reantils { get; set; }
    public DbSet<EventModelEntity> Events { get; set; }
    public DbSet<HisoryEventsModelEntity> HistoryEvents { get; set; }
    public DbSet<TeamEntity> Teams { get; set; }

    public DbSet<EventsForAllCommandsModelEntity> EventsForAllCommands { get; set; }    
    public DbSet<PlayerStatisticsModelEntity> PlayerStatistics { get; set; }

    public DbSet<DeviceRegistartionModelEntity> DeviceRegistartionModelEntities { get; set; }
    public DbSet<NotificationEntity> Notifications { get; set; }



    static SquadDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    public SquadDbContext(DbContextOptions<SquadDbContext> options) : base (options)
    {        
       
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserModelEntityConfiguration());
        modelBuilder.ApplyConfiguration(new PolygonEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EventModelEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ReantalEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentEntityConfiguration());
        modelBuilder.ApplyConfiguration(new HisoryEventsModelEntityConfiguration());
        modelBuilder.ApplyConfiguration(new TeamEntityConfiguration());

        modelBuilder.ApplyConfiguration(new DeviceRegistartionModelConfig());
        modelBuilder.ApplyConfiguration(new NotificationEntityConfig());

        modelBuilder.ApplyConfiguration(new PlayerStatisticsModelEntityConfig());
        modelBuilder.ApplyConfiguration(new EventsForAllCommandsModelEntityConfig());
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Host=localhost;Port=5432;Username=HSquadAdmin;Password=42924870;Database=SquadDBPostgres;";
        optionsBuilder.UseSqlServer(connectionString);
    }


}
