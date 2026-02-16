namespace SquadServer.Models.ModelsConfig;

public class PlayerStatisticsModelEntityConfig : IEntityTypeConfiguration<PlayerStatisticsModelEntity>
{
    public void Configure(EntityTypeBuilder<PlayerStatisticsModelEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.
            HasOne(p => p.UserModel).
            WithOne(p => p.Statistic).
            HasForeignKey<PlayerStatisticsModelEntity>(u=>u.UserModelId);

        builder.Ignore(e => e.Achievements);
    }
}
