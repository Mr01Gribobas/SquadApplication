namespace SquadServer.Models;

public  class EquipmentEntity
{
    public int Id { get; set; }//Id 
    public bool MainWeapon { get; set; }// Основное оружие 
    public string? NameMainWeapon { get; set; }//Название
    public bool SecondaryWeapon { get; set; }//Вторичка
    public string? NameSecondaryWeapon { get; set; }//Название


    
    public bool HeadEquipment { get; set; }//Защита головы
    public bool BodyEquipment { get; set; }//Защита тела
    public bool UnloudingEquipment { get; set; }//Разгрузка

    public int OwnerEquipmentId { get; set; }

    [JsonIgnore]
    public virtual UserModelEntity OwnerEquipment { get; set; } = null!;

    public static EquipmentEntity CreateModelEntity(EquipmentDTO? equipFromApp)
    {
        EquipmentEntity equipment = new EquipmentEntity() 
        {
            MainWeapon = equipFromApp?.MainWeapon ?? false,
            NameMainWeapon = equipFromApp?.NameMainWeapon?? "нет" ,

            SecondaryWeapon = equipFromApp?.SecondaryWeapon ?? false,
            NameSecondaryWeapon = equipFromApp?.NameSecondaryWeapon ?? "нет",

            BodyEquipment = equipFromApp?.BodyEquipment ?? false,
            HeadEquipment = equipFromApp?.HeadEquipment ?? false,
            UnloudingEquipment = equipFromApp?.UnloudingEquipment ?? false,
        };
        return equipment;
    }
}
