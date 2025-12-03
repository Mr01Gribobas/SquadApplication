using SquadApplication.Repositories;

namespace SquadApplication.Models.EntityModels;

public class UserModel
{
    public int Id { get; set; }
    private Role? _role;//роль
    public string _callSing { get; private set; } = null!;  //позывной
    public string _teamName { get; private set; } = null!;//team
    public string _phoneNumber { get; private set; } = null!;// number
    public string? _userName { get; private set; }//имя
    public int? _age { get; private set; }//возраст
    public bool? _isStaffed { get; private set; }//укомплектованность
    public bool? _goingToTheGame { get; set; }//явка на игру 


    public int? EquipmentId { get; set; }
    public Equipment _equipment;//снаряжение




    public static bool CreateUserEntity(string _teamName,string _name, string _callSing, string _phone, Role? _role, int? _age)
    {
        UserModel newUser = new UserModel()
        {
            _teamName = _teamName,
            _userName = _name,
            _role = _role,
            _callSing = _callSing,
            _phoneNumber = _phone,
            _age = _age,
        };

        DataBaseManager baseManager = new DataBaseManager();
        baseManager.SendDataForEnter(DataFor.Registration,newUser);
        return true;
    }











    public static List<UserModel> GetRandomData()
    {
        UserModel userModel = new UserModel();
        userModel._userName = "Maks";
        userModel.Id = 1;
        userModel._isStaffed = false;
        UserModel userMode2 = new UserModel();
        userMode2._userName = "Roma";
        userMode2.Id = 2;
        userMode2._isStaffed = true;
        UserModel userMode3 = new UserModel();
        userMode3._userName = "Sasha";
        userMode3.Id = 3;
        userMode3._isStaffed = false;
        UserModel userMode4 = new UserModel();
        userMode4._userName = "Fama";
        userMode4.Id = 4;
        userMode4._isStaffed = true;
        var list = new List<UserModel>();
        list.AddRange(userModel, userMode2, userMode3, userMode4);
        return list;
    }

}


