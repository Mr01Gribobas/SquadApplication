
namespace SquadApplication.ViewModels;

public partial class YourEquipViewModel:ObservableObject
{
    private UserModelEntity _user;
    public YourEquipViewModel(YourEquipPage page , UserModelEntity user)
    {
        _user = user;
        if(_user is not null )
        {
            InitialProperty();
        }
    }

    private void InitialProperty()
    {
        Name = _user._userName;
        CallSing = _user._callSing;
        Role = _user._role.ToString();
        PhoneNumber = _user._phoneNumber;
        Age = _user._age?.ToString();
        IsStaffed = _user._isStaffed.ToString();
        TeamName = _user._teamName;
        EquipmentId = _user.EquipmentId?.ToString();
    }

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
}
