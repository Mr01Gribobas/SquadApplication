
using SquadApplication.Repositories.ManagerRequest.Interfaces;

namespace SquadApplication.ViewModels;

public partial class AuthorizeViewModel : ObservableObject
{
    private AuthorizedPage _authorizedPage;
    private readonly IDeviceManager _deviceManager;
    private IRequestManagerForEnter _requestManager;

    [ObservableProperty]
    private string accesCode;

    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string callSing;

    [ObservableProperty]
    private string team;

    [ObservableProperty]
    private string phoneNumber;

    public AuthorizeViewModel(AuthorizedPage authorizedPage,
        IUserSession userSession,
        IDeviceManager deviceMagager)
    {
        _authorizedPage = authorizedPage;
        _deviceManager = deviceMagager;
        _requestManager = new DataBaseManager(userSession, deviceMagager);
    }


    [RelayCommand]
    private async Task Login()
    {
        if(AccesCode is null || AccesCode.Length <= 0)
        {
            return;
        }
        DataBaseManager requestManager = (DataBaseManager)_requestManager;
        UserModelEntity? userFromResponce = await requestManager.SendDataForEnter(AccesCode);
        if(userFromResponce is null | userFromResponce?.Id <= 0)
        {
            await _authorizedPage.DisplayAlertAsync("Error", "Пользователя с таким кодом нету!", "Ok");
            return;
        }
        await Shell.Current.GoToAsync($"/{nameof(MainPage)}/?UserId={userFromResponce.Id}");
    }

    


    [RelayCommand]
    private async Task Registration()
    {

        if(!ValidateData())
            return;

        UserModelEntity newUser = UserModelEntity.CreateUserEntity(
            _teamName: Team,
            _name: Name,
            _callSing: CallSing,
            _phone: PhoneNumber,
            _age: null,
            _role: Role.Private,
            _teamId: null
            );
    SendUserData:
        DataBaseManager requestManager = (DataBaseManager)_requestManager;
        UserModelEntity? userFromResponce = await requestManager.SendDataForRegistration(newUser);
        if(userFromResponce is null | userFromResponce?.Id <= 0)
        {
            if(requestManager.GetStatusCode() == 201)
            {
                bool result = await _authorizedPage.DisplayAlertAsync("Команда которую вы выбрали не существует !", "Желаете её создать и стать её командиром ?", "Yes", "No");
                if(result)
                {
                    newUser = UserModelEntity.CreateUserEntity(
                  _teamName: Team,
                  _name: Name,
                  _callSing: CallSing,
                  _phone: PhoneNumber,
                  _age: null,
                  _role: Role.Commander,
                  _teamId: null
                  );
                }
                else
                {
                    return;
                }
                goto SendUserData;
            }
            await _authorizedPage.DisplayAlertAsync("Error data","Invalid data", "Ok");
            return;
        }

        await _authorizedPage.DisplayAlertAsync("Kode",$"Ваш код доступа {userFromResponce._enterCode}", "Ok");
        await Shell.Current.GoToAsync($"/{nameof(MainPage)}/?UserId = {userFromResponce.Id}");
    }


    private bool ValidateData()
    {
        var data = new DataForm
        {
            _name = Name,
            _callSing = CallSing,
            _phoneNumber = PhoneNumber,
            _team = Team,
        };

        if(!data.CheckProperty())
            return false;

        if(data._phoneNumber[0] == '+')
        {
            string skipPlus = new String(data._phoneNumber?.Skip(1).ToArray());
            if(!Int64.TryParse(skipPlus, out Int64 _)  )
            {
                return false;
            }
        }
        else if(!Int64.TryParse(data._phoneNumber, out Int64 _) )
        {
            return false;
        }

        if(data._name.Length > 50 | data._name.Length < 2)
        {
            return false;
        }
        if(data._callSing.Length > 50 | data._callSing.Length < 2)
        {
            return false;
        }
        if(data._team.Length > 100 | data._team.Length < 2)
        {
            return false;
        }
        return true;
    }
    private record DataForm
    {
        public string _accesCode;

        public string _name;

        public string _callSing;

        public string _team;

        public string _phoneNumber;
        public bool CheckProperty()
        {
            if(_callSing is null)
                return false;
            if(_name is null)
                return false;
            if(_team is null)
                return false;
            if(_phoneNumber is null || _phoneNumber.Length > 20)
                return false;
            return true;
        }
    }



}
