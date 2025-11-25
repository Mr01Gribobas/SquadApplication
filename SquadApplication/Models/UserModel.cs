namespace SquadApplication.Models;

public class UserModel
{
    public int Id { get; set; }
    private Role _role;//роль
    public string _callSing { get; private set; }//позывной
    public string? _userName { get; private set; }//имя
    public int? _age { get; private set; }//возраст
    public bool? _isStaffed { get; private set; }//укомплектованность

    public int? EquipmentId { get; set; }
    private Equipment _equipment;//снаряжение

    public static List<UserModel> GetRandomData()
    {
        UserModel userModel = new UserModel();
        userModel._userName = "Maks";
        userModel.Id = 1;
        UserModel userMode2 = new UserModel();
        userModel._userName = "Roma";
        userModel.Id = 2;
        UserModel userMode3 = new UserModel();
        userModel._userName = "Sasha";
        userModel.Id = 3;
        UserModel userMode4 = new UserModel();
        userModel._userName = "Fama";
        userModel.Id = 4;
        var list = new List<UserModel>();
        list.AddRange(userModel, userMode2,userMode3,userMode4);
        return list;
    }

}


