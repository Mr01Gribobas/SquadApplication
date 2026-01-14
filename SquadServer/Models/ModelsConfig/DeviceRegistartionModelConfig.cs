namespace SquadServer.Models.ModelsConfig;

public class DeviceRegistartionModelConfig : IEntityTypeConfiguration<DeviceRegistartionModelEntity>
{
    public void Configure(EntityTypeBuilder<DeviceRegistartionModelEntity> builder)
    {
        builder.HasKey(k=>k.Id);

        builder.HasOne(d=>d.User);

        builder.Property(p=>p.DeviceToken).HasMaxLength(500);

    }
}
