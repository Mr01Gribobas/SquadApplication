namespace SquadApplication.DTO_Classes.DTO_AuxiliaryModels;

public record EquipmentDTO
{
    public bool MainWeapon { get; set; }// Основное оружие 
    public string? NameMainWeapon { get; set; }//Название
    public bool SecondaryWeapon { get; set; }//Вторичка
    public string? NameSecondaryWeapon { get; set; }//Название



    public bool HeadEquipment { get; set; }//Защита головы
    public bool BodyEquipment { get; set; }//Защита тела
    public bool UnloudingEquipment { get; set; }//Разгрузка

    public int OwnerEquipmentId { get; set; }
}
