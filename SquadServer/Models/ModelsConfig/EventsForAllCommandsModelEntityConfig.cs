namespace SquadServer.Models.ModelsConfig;

public class EventsForAllCommandsModelEntityConfig : IEntityTypeConfiguration<EventsForAllCommandsModelEntity>
{
    public void Configure(EntityTypeBuilder<EventsForAllCommandsModelEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasMany(p => p.Players);
    }
}
