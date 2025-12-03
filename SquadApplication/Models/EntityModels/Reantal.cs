namespace SquadApplication.Models.EntityModels;

public class Reantal
{
    public int Id { get; set; }
    public bool Weapon { get; set; }// оружие
    public bool Mask { get; set; }//Маска
    public bool Helmet { get; set; }//шлем
    public bool Balaclava { get; set; }//балаклава
    public bool SVMP { get; set; }//нижний слой защиты
    public bool Outterwear { get; set; }//верхняя одежда
    public bool Gloves { get; set; }//перчатки двойные
    public bool BulletproofVestOrUnloadingVest { get; set; }//плитник\разгруз
}

