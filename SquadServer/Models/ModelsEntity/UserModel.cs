using System.Data;

namespace SquadServer.Models;

public class UserModelEntity
{
    public int Id { get; set; }
    public Role _role { get; private set; } //роль
    public string _callSing { get; private set; } = null!;  //позывной
    public string _teamName { get; private set; } = null!;//team
    public string _phoneNumber { get; private set; } = null!;// number
    public string? _userName { get; private set; }//имя
    public int? _age { get; private set; }//возраст
    public bool? _isStaffed { get; private set; }//укомплектованность
    public bool? _goingToTheGame { get; set; }//явка на игру 


    public int? EquipmentId { get; set; }
    public virtual EquipmentEntity? Equipment { get; set; }//снаряжение

    public int? TeamId { get; set; }
    public virtual TeamEntity? Team { get; set; }



    public static UserModelEntity CreateUserEntity(string _teamName, string _name, string _callSing, string _phone, Role _role, int? _age)
    {

        UserModelEntity newUser = new UserModelEntity()
        {
            _teamName = _teamName,
            _userName = _name,
            _role = _role,
            _callSing = _callSing,
            _phoneNumber = _phone,
            _age = _age,
            TeamId = 1
        };
        
        return newUser;
    }


}


