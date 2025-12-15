

namespace SquadApplication.ViewModels;

public partial class YourEquipViewModel : ObservableObject
{
    private UserModelEntity _user;
    public YourEquipViewModel(YourEquipPage page, UserModelEntity user)
    {
        _user = user;
        if(_user is not null)
        {
            InitialPropertyUser();
            if(_user.EquipmentId is not null | _user.EquipmentId > 0)
            {
                InitialPropertyEquip();
            }
        }
    }

    private void InitialPropertyEquip()
    {
        //throw new NotImplementedException();
    }

    private void InitialPropertyUser()
    {
        Name = _user._userName;
        CallSing = _user._callSing;
        Role = _user._role.ToString();
        PhoneNumber = _user._phoneNumber;
        TeamName = _user._teamName;
        Age = _user._age == null | _user._age <= 0 ? "Не установоено" : _user._age.ToString();
        IsStaffed = _user._isStaffed == null | _user._isStaffed is false ? " Не укомплектован " : " Укомплектован";
        EquipmentId = _user.EquipmentId is null | _user.EquipmentId <= 0 ? "Нету зарегистрированных екипов" : _user.EquipmentId.ToString();
    }

    //==============================
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string callSing;

    [ObservableProperty]
    private string role;

    [ObservableProperty]
    private string phoneNumber;

    [ObservableProperty]
    private string age;

    [ObservableProperty]
    private string isStaffed;

    [ObservableProperty]
    private string teamName;

    [ObservableProperty]
    private string equipmentId;
    //==============================
    [ObservableProperty]
    private string mainWeapon;

    [ObservableProperty]
    private string secondaryWeapon;

    [ObservableProperty]
    private string headEquipment;

    [ObservableProperty]
    private string bodyEquipment;

    [ObservableProperty]
    private string unloudingWeapon;
    //==============================
    [ObservableProperty]
    private string nameTeam;

    [ObservableProperty]
    private string countMembers;

    [ObservableProperty]
    private string isEvent;
}
