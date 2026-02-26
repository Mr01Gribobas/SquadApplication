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
            MainWeapon = equipFromApp.MainWeapon,
            NameMainWeapon = equipFromApp.NameMainWeapon,

            SecondaryWeapon = equipFromApp.SecondaryWeapon,
            NameSecondaryWeapon = equipFromApp.NameSecondaryWeapon,

            BodyEquipment = equipFromApp.BodyEquipment,
            HeadEquipment = equipFromApp.HeadEquipment,
            UnloudingEquipment = equipFromApp.UnloudingEquipment,
        };
        return equipment;
    }
}
