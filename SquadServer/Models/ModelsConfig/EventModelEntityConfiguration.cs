namespace SquadServer.Models.ModelsConfig;

public class EventModelEntityConfiguration : IEntityTypeConfiguration<EventModelEntity>
{
    public void Configure(EntityTypeBuilder<EventModelEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.
            HasOne(e => e.Team).
            WithOne(t => t.Event).
            HasForeignKey<TeamEntity>(k=>k.EventId).
            OnDelete(DeleteBehavior.SetNull);
    }
}
