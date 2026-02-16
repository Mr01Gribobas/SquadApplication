using System.Text.Json.Serialization;

namespace SquadServer.Models;

public class UserModelEntity
{
    public int Id { get; set; }

    [JsonInclude]
    public Role _role { get; private set; } //роль

    [JsonInclude]
    public string _callSing { get; private set; } = null!;  //позывной

    [JsonInclude]
    public string _teamName { get; private set; } = null!;//team

    [JsonInclude]
    public string _phoneNumber { get; private set; } = null!;// number

    [JsonInclude]
    public string? _userName { get; private set; }//имя

    [JsonInclude]
    public int? _age { get; private set; }//возраст

    [JsonInclude]
    public bool? _isStaffed { get; private set; }//укомплектованность\

    [JsonInclude]
    public DateTime _dataRegistr {  get; private set; }


    public Int64 _enterCode { get; set; }
    public bool? _goingToTheGame { get; set; }//явка на игру 


    public int? EquipmentId { get; set; }

    [JsonIgnore]
    public virtual EquipmentEntity? Equipment { get; set; }//снаряжение

    public int? TeamId { get; set; }

    [JsonIgnore]
    public virtual TeamEntity? Team { get; set; }


    public int? StatisticId { get; set; }

    [JsonIgnore]
    public virtual PlayerStatisticsModelEntity? Statistic { get; set; } = null!;
    

    public static UserModelEntity CreateUserEntity(string _teamName, string _name, string _callSing, string _phone, Role _role, int? _age, int? _teamId)
    {

        UserModelEntity newUser = new UserModelEntity()
        {
            _teamName = _teamName,
            _userName = _name,
            _role = _role,
            _callSing = _callSing,
            _phoneNumber = _phone,
            _age = _age,
            _enterCode = GenerationCode(_phone),
            _dataRegistr = DateTime.UtcNow,
            TeamId = _teamId
        };


        return newUser;
    }

    
    
    public static void UpdateProfile(UserModelEntity userFromApp, UserModelEntity userEntity)
    {
        userEntity._userName = userFromApp._userName;
        userEntity._callSing = userFromApp._callSing;
        userEntity._role = userFromApp._role;
        userEntity._phoneNumber = userFromApp._phoneNumber;
        userEntity._teamName = userFromApp._teamName;
        userEntity._age = userFromApp._age;
        userEntity.TeamId = userFromApp.TeamId;
    }
    public bool UpdateStaffed(EquipmentEntity equip)
    {        
        if(
                equip.BodyEquipment &
                equip.HeadEquipment &
                equip.UnloudingEquipment &
                equip.MainWeapon &
                equip.NameMainWeapon != string.Empty
                )
        {
            this._isStaffed = true;            
            return true;
        }
        this._isStaffed = false;
        return false;

    }
    private static Int64 GenerationCode(string phuneNumber)
    {
        if(phuneNumber is null)
            throw new ArgumentNullException();

        char[] skipsNumbers = phuneNumber.Skip(5).ToArray();
        string newString = new string(skipsNumbers);

        if(!int.TryParse(newString, out int code))
            throw new FormatException();

        return code;
    }

}


