
namespace SquadServer.Models.ModelsConfig;

public class PolygonEntityConfiguration : IEntityTypeConfiguration<PolygonEntity>
{
    public void Configure(EntityTypeBuilder<PolygonEntity> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
