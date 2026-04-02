using SquadApplication.Repositories.ManagerRequest.UpgradeRequestManager;

namespace SquadApplication.ViewModels;

public partial class EditYourProfileViewModel : ObservableObject
{
    private BaseRequestsManager _requestManager;
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
        _requestManager = new BaseRequestsManager(_editProfilePage._clientFactory.CreateClient());
        InitalProperty(_user);
    }

    private void InitalProperty(UserModelEntity user)
    {
        if(user is null)
            return;
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
        var dataForm = new DataForm(
            _name: Name,
            _callSing: CallSing,
            _age: Age,
            _role: Role,
            _phoneNumber: PhoneNumber,
            _teamName: TeamName
            );
        
        if(dataForm._teamName != _user._teamName )
        {
            bool resultFromUser = await _editProfilePage.DisplayAlertAsync("Предупреждение ", "Вы действительно желаете сменить команду  ??", "Да", "Нет");
            if(!resultFromUser)
                return;
        }

        if(!ValidateDataUser(dataForm))
            await _editProfilePage.DisplayAlertAsync("Error", "Некоректные данные ", "Ok");
        UserModelEntity newUser = UserModelEntity.CreateUserEntity(
            _name: dataForm._name,
            _callSing: dataForm._callSing,
            _role: _selectedRole,
            _phone: dataForm._phoneNumber,
            _age: int.Parse(dataForm._age),
            _teamName: dataForm._teamName,
            _teamId: _user.TeamId
            );
        bool result = false;
        if(_user is not null)
        {
            _requestManager.SetAddress($"api/users/UpdateShorPtofile?userId={_user.Id}");
            result = await _requestManager.PatchDateAsync<UserModelEntity>(newUser);
        }
        _requestManager.ResetAddress();
        if(!result)
            await _editProfilePage.DisplayAlertAsync("Error", "Ошибка обновления профиля ", "Ok");
        await Shell.Current.GoToAsync($"/{nameof(HomePage)}");
    }

    private bool ValidateDataUser(DataForm dataForm)
    {
        if(dataForm._phoneNumber is not null && dataForm._phoneNumber[0] == '+')
        {
            string skipPlus = new String(dataForm._phoneNumber?.Skip(1).ToArray());
            if(dataForm._phoneNumber is null || !Int64.TryParse(skipPlus, out Int64 _))
                return false;
        }
        else if(dataForm._phoneNumber is null || !Int64.TryParse(dataForm._phoneNumber, out Int64 _))
            return false;
        if(!int.TryParse(dataForm._age, out int _))
            return false;
        else if(int.TryParse(dataForm._age, out int age))
        {
            if(age > 200 || age <= 0)
                return false;
        }
        if(dataForm._teamName is null | dataForm._teamName.Length > 100 | dataForm._teamName.Length <= 0)
            return false;
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