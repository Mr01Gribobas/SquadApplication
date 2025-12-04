namespace SquadServer.Models.ModelsConfig;

public class UserModelEntityConfiguration : IEntityTypeConfiguration<UserModelEntity>
{
    public void Configure(EntityTypeBuilder<UserModelEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
    }
}
