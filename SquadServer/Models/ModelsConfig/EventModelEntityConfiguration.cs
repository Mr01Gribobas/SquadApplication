namespace SquadServer.Models.ModelsConfig;

public class EventModelEntityConfiguration : IEntityTypeConfiguration<EventModelEntity>
{
    public void Configure(EntityTypeBuilder<EventModelEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
