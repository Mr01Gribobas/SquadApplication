
namespace SquadApplication.ViewModels;

public partial class EditYourProfileViewModel : ObservableObject
{

    public EditYourProfileViewModel(EditUserProfilePage profilePage, UserModelEntity user)
    {
        _user = user;
        _editProfilePage = profilePage;
        InitalProperty(_user);
        _requestManager = new ManagerPostRequests<UserModelEntity>();
    }

    private void InitalProperty(UserModelEntity user)
    {
        if(user is null)
        {
            return;
        }
        Name = user._userName;
        CallSing = user._callSing;
        Role = user._role.ToString();
        PhoneNumber = user._phoneNumber;
        TeamName = user._teamName;
        Age = user?._age.ToString() ?? "Не указан";
    }

    private IRequestManager<UserModelEntity> _requestManager;
    public EditUserProfilePage _editProfilePage;
    private UserModelEntity _user;


    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string callSing;


    [ObservableProperty]
    private string role;

    private Role _selectedRole;

    [ObservableProperty]
    private string phoneNumber;

    [ObservableProperty]
    private string age;


    [ObservableProperty]
    private string teamName;



    [RelayCommand]
    private async Task UpdateProfile()
    {
        if(!ValidateDataUser())
        {
            return;//error
        }

        UserModelEntity newUser = UserModelEntity.CreateUserEntity(
            _name: Name,
            _callSing: CallSing,
            _role: _selectedRole,
            _phone: PhoneNumber,
            _age: int.Parse(Age),
            _teamName: TeamName,
            _teamId: _user.TeamId
            );


        var requestManager = (ManagerPostRequests<UserModelEntity>)_requestManager;
        if(requestManager is null)
        {
            throw new NullReferenceException();
        }
        if(_user is not null)
        {

            requestManager.SetUrl($"UpdateProfile?userId={_user.Id}");
            requestManager?.PostRequests(objectValue: newUser, PostsRequests.UpdateProfile);
        }
        requestManager.ResetUrl();
        await Shell.Current.GoToAsync($"/{nameof(YourEquipPage)}");

    }
    private bool ValidateDataUser()
    {
        if(PhoneNumber[0] == '+')
        {
            string skipPlus = new String(PhoneNumber?.Skip(1).ToArray());
            if(PhoneNumber is null | !Int64.TryParse(skipPlus, out Int64 number))
            {
                //
                return false;
            }
        }
        else if(PhoneNumber is null | !Int64.TryParse(PhoneNumber, out Int64 number))
        {
            return false;
        }
        if(!int.TryParse(Age, out int resultParse))
        {
            return false;
        }
        else if(int.TryParse(Age, out int age))
        {
            if(age > 200 || age <= 0)
            {
                return false;
            }
        }
        if(TeamName is null | TeamName.Length > 100 | TeamName.Length <= 0)
        {
            return false;
        }
        switch(Role)
        {
            case "Командир":
                _selectedRole = Models.Role.Commander;
                break;
            case "Помощник командира":
                _selectedRole = Models.Role.AssistantCommander;
                break;
            case "Рядовой ":
                _selectedRole = Models.Role.Private;
                break;
            case "Механик":
                _selectedRole = Models.Role.Mechanic;
                break;
            default:
                _selectedRole = _user._role;
                break;
        }


        return true;
    }

}
