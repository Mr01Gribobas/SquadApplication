namespace SquadServer.Models.ModelsConfig;

public class ReantalEntityConfiguration : IEntityTypeConfiguration<ReantalEntity>
{
    public void Configure(EntityTypeBuilder<ReantalEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
