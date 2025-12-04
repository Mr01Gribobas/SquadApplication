namespace SquadServer.Models.ModelsConfig;

public class EquipmentEntityConfiguration : IEntityTypeConfiguration<EquipmentEntity>
{
    public void Configure(EntityTypeBuilder<EquipmentEntity> builder)
    {
        builder.HasKey(x => x.Id);

        // Устанавливаем уникальный индекс, чтобы у одного пользователя
        // не могло быть несколько инвентарей
        builder.HasIndex(x => x.OwnerEquipmentId).IsUnique();

        builder.Property(x => x.NameMainWeapon).HasMaxLength(50);
        
        
    }

}
