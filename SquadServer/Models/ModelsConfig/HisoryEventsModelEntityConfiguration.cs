namespace SquadServer.Models.ModelsConfig;

public class HisoryEventsModelEntityConfiguration : IEntityTypeConfiguration<HisoryEventsModelEntity>
{
    public void Configure(EntityTypeBuilder<HisoryEventsModelEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
