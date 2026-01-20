namespace SquadServer.Models.ModelsConfig;

public class NotificationEntityConfig : IEntityTypeConfiguration<NotificationEntity>
{
    public void Configure(EntityTypeBuilder<NotificationEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
