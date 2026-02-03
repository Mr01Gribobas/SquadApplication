using Android.Provider;

namespace SquadApplication.ViewModels;

public partial class AuthorizeViewModel : ObservableObject
{
    private AuthorizedPage _authorizedPage;
    private readonly IDeviceManager _deviceManager;
    private IRequestManagerForEnter _requestManager;
    public AuthorizeViewModel(AuthorizedPage authorizedPage,
        IUserSession userSession,
        IDeviceManager deviceMagager)
    {
        _authorizedPage = authorizedPage;
        _deviceManager = deviceMagager;
        _requestManager = new DataBaseManager(userSession, deviceMagager);
    }

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
    public async Task TestMethod()
    {
        await TestWork();
    }
    private async Task TestWork()
    {
        var devicePlatfom = DeviceInfo.Platform;
        var deviceModel = DeviceInfo.Model;
        var deviceType = DeviceInfo.DeviceType;
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
                    await Shell.Current.GoToAsync($"..");
                }
                goto SendUserData;
            }
            // invalide data
            return;
        }
        Shell.Current.GoToAsync($"/{nameof(MainPage)}/?UserId = {userFromResponce.Id}");
        //выводить код авторизации пользователя 
    }


    private bool ValidateData()
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
        else if(Name is null | Name.Length > 50 | Name.Length < 2)
        {
            return false;
        }
        else if(CallSing is null | CallSing.Length > 50 | CallSing.Length < 2)
        {
            return false;
        }
        else if(Team is null | Team.Length > 100 | Team.Length < 2)
        {
            return false;
        }
        return true;
    }
    private 
}
