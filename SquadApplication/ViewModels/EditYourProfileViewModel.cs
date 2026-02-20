namespace SquadApplication.ViewModels;

public partial class EditYourProfileViewModel : ObservableObject
{

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

   



    [RelayCommand]
    private async Task UpdateProfile()
    {
        var dataForm =new DataForm(
            _name:Name,
            _callSing:CallSing,
            _age: Age ,
            _role:Role,
            _phoneNumber:PhoneNumber,
            _teamName:TeamName         
            );

        if(!ValidateDataUser(dataForm))
        {
            return;//error
        }

        UserModelEntity newUser = UserModelEntity.CreateUserEntity(
            _name: dataForm._name,
            _callSing: dataForm._callSing,
            _role: _selectedRole,
            _phone: dataForm._phoneNumber,
            _age: int.Parse(dataForm._age),
            _teamName: dataForm._teamName,
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
        requestManager.ResetUrlAndStatusCode();
        await Shell.Current.GoToAsync($"/{nameof(HomePage)}");

    }
    private bool ValidateDataUser(DataForm dataForm)
    {
        if(dataForm._phoneNumber is not null && dataForm._phoneNumber[0] == '+')
        {
            string skipPlus = new String(dataForm._phoneNumber?.Skip(1).ToArray());
            if(dataForm._phoneNumber is null || !Int64.TryParse(skipPlus, out Int64 _))
            {
                return false;
            }
                
        }
        else if(dataForm._phoneNumber is null || !Int64.TryParse(dataForm._phoneNumber, out Int64 _))
        {
            return false;
        }
        if(!int.TryParse(dataForm._age, out int _))
        {
            return false;
        }
        else if(int.TryParse(dataForm._age, out int age))
        {
            if(age > 200 || age <= 0)
            {
                return false;
            }
        }
        if(dataForm._teamName is null | dataForm._teamName.Length > 100 | dataForm._teamName.Length <= 0)
        {
            return false;
        }
        switch(dataForm._role)
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

    private record DataForm(
        
        string _name,
         string _callSing,
         string _teamName,
         string _phoneNumber,
         string _role,
         string _age
        );
}