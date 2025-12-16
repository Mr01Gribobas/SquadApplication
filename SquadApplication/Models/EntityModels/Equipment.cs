using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace SquadApplication.Models.EntityModels;

public class EquipmentEntity
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



    public static EquipmentEntity CreateEquipment(
        bool mainWeapon,
        bool secondaryWeapon,
        bool headEq,
        bool bodyEq,
        bool unloudingEq,
        string nameMainWeapon,
        UserModelEntity owner,
        string secondaryNameWeapon = null
        )
    {
        if(owner is null )
        {
            return null;
        }
        var equipment = new EquipmentEntity()
            {
                MainWeapon = mainWeapon,
                SecondaryWeapon = secondaryWeapon,
                HeadEquipment = headEq,
                BodyEquipment = bodyEq,
                UnloudingEquipment = unloudingEq,

                NameMainWeapon = nameMainWeapon,
                NameSecondaryWeapon = secondaryNameWeapon,

                OwnerEquipmentId = owner.Id,
                OwnerEquipment = owner,
            };
        return equipment;
    }
    

    }
