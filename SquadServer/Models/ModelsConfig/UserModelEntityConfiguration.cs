namespace SquadServer.Models.ModelsConfig;

public class UserModelEntityConfiguration : IEntityTypeConfiguration<UserModelEntity>
{
    public void Configure(EntityTypeBuilder<UserModelEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.
            HasOne(u=>u.Equipment).
            WithOne(e=>e.OwnerEquipment).
            HasForeignKey<UserModelEntity>(k=>k.EquipmentId).
            OnDelete(DeleteBehavior.Cascade);
        

        builder.
            HasOne(t=>t.Team).
            WithMany(u=>u.Members).
            HasForeignKey(u => u.TeamId).
            IsRequired(false);

        builder.
            HasOne(s=>s.Statistic).
            WithOne(p=>p.UserModel).
            HasForeignKey<PlayerStatisticsModelEntity>(k=>k.UserModelId).
            OnDelete(DeleteBehavior.Cascade);
    }
}
