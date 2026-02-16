using System.Text.Json.Serialization;

namespace SquadApplication.Models.EntityModels;

public class UserModelEntity
{
    public int Id { get; set; }
    [JsonInclude]
    public Role _role { get; private set; }
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
    public bool? _isStaffed { get; private set; }//укомплектованность

    [JsonInclude]
    public DateTime _dataRegistr { get; private set; }



    public bool? _goingToTheGame { get; set; }//явка на игру 
    public Int64 _enterCode { get; set; }

    public int? EquipmentId { get; set; }

    [JsonIgnore]
    public EquipmentEntity _equipment;//снаряжение


    public int? TeamId { get; set; }
    [JsonIgnore]
    public virtual TeamEntity? Team { get; set; }



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
            _enterCode = 0,
            TeamId = _teamId
        };


        return newUser;
    }
    public static List<UserModelEntity> GetRandomData()
    {
        UserModelEntity userModel = new UserModelEntity();
        userModel._userName = "Maks";
        userModel.Id = 1;
        userModel._isStaffed = false;
        UserModelEntity userMode2 = new UserModelEntity();
        userMode2._userName = "Roma";
        userMode2.Id = 2;
        userMode2._isStaffed = true;
        UserModelEntity userMode3 = new UserModelEntity();
        userMode3._userName = "Sasha";
        userMode3.Id = 3;
        userMode3._isStaffed = false;
        UserModelEntity userMode4 = new UserModelEntity();
        userMode4._userName = "Fama";
        userMode4.Id = 4;
        userMode4._isStaffed = true;
        var list = new List<UserModelEntity>();
        list.AddRange(userModel, userMode2, userMode3, userMode4);
        return list;
    }

}


