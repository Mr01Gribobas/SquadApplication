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



    public static void UpdateEquip(EquipmentEntity equipFromApp, EquipmentEntity equipEntity)
    {
        equipEntity.OwnerEquipment = equipFromApp.OwnerEquipment;
        equipEntity.OwnerEquipmentId = equipFromApp.OwnerEquipmentId;
        equipEntity.UnloudingEquipment = equipFromApp.UnloudingEquipment;
        equipEntity.HeadEquipment = equipFromApp.HeadEquipment;
        equipEntity.BodyEquipment = equipFromApp.BodyEquipment;
        equipEntity.MainWeapon = equipFromApp.MainWeapon;
        equipEntity.SecondaryWeapon = equipFromApp.SecondaryWeapon;
    }
}
