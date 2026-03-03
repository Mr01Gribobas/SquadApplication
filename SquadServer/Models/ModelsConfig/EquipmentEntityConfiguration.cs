namespace SquadServer.Models.ModelsConfig;

public class EquipmentEntityConfiguration : IEntityTypeConfiguration<EquipmentEntity>
{
    public void Configure(EntityTypeBuilder<EquipmentEntity> builder)
    {
        builder.HasKey(x => x.Id);


        builder.HasOne(e => e.OwnerEquipment).
            WithOne(u=>u.Equipment).
            HasForeignKey<UserModelEntity>(u=>u.EquipmentId);
        

        builder.HasIndex(x => x.OwnerEquipmentId).IsUnique();
        builder.Property(x => x.NameMainWeapon).HasMaxLength(50);
        
        
    }

}
