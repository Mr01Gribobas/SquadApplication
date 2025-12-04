
namespace SquadServer.Models.ModelsConfig;

public class TeamEntityConfiguration : IEntityTypeConfiguration<TeamEntity>
{
    public void Configure(EntityTypeBuilder<TeamEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .HasMany(t=>t.Members)
            .WithOne(u=>u.Team)
            .HasForeignKey(u => u.TeamId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
