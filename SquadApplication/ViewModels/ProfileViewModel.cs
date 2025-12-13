
namespace SquadApplication.ViewModels;

public partial class ProfileViewModel : ObservableObject
{
    private UserModelEntity _user;
    public ProfileViewModel(UserModelEntity userModel) 
    {
        _user = userModel;
        SetProfile(_user);
    }

    private void SetProfile(UserModelEntity user)
    {
        if(user is null)
        {
            return;
        }

        CallSing = user._callSing;
        UserName = user._userName;
        TeamName = user._teamName;
        PhoneNumber = user._phoneNumber;
        Role =  user._role.ToString();
        Age = user._age.ToString();
        IsStaffed = user._isStaffed;
        GoingToTheGame = user._goingToTheGame;
        EquipmentId = user.EquipmentId;
    }

    [ObservableProperty]
    private string callSing; 

    [ObservableProperty]
    private string userName;

    [ObservableProperty]
    private string teamName;

    [ObservableProperty]
    private string phoneNumber;

    [ObservableProperty]
    private string role;

    [ObservableProperty]
    private string age;

    [ObservableProperty]
    private bool? isStaffed;

    [ObservableProperty]
    private bool? goingToTheGame;

    [ObservableProperty]
    private int? equipmentId;

}
