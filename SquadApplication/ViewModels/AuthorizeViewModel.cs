using SquadApplication.Repositories;
using SquadApplication.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SquadApplication.ViewModels;


public partial class AuthorizeViewModel  : ObservableObject
{
    private AuthorizedPage _authorizedPage;
    private IRequestManagerForEnter _requestManager;
    public AuthorizeViewModel(AuthorizedPage authorizedPage)
    {
        _authorizedPage = authorizedPage;
        _requestManager = new DataBaseManager();
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
    private string phoneNumber ;

    [RelayCommand]
    private void Login()
    {
        if(AccesCode.Length <= 0  )
        {
            return;
        }
        DataBaseManager requestManager = (DataBaseManager)_requestManager;
        UserModelEntity? responce = requestManager.SendDataForEnter(AccesCode).Result ;
        if(responce is null)
        {            
            //Error
        }
        Shell.Current.GoToAsync($"/{nameof(MainPage)}");//  ?UserId=responce.Id
    }


    [RelayCommand]
    private async Task Registration()
    {
      bool resilt = await  _authorizedPage.DisplayAlertAsync("Команда которую вы выбрали не существует !","Желаете её создать и стать её командиром ?","Yes","No");
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
        UserModelEntity responce = requestManager.SendDataForRegistration(newUser).Result;
        if(responce is null)
        {
            if(requestManager.GetStatusCode()== 201)
            {
                //no team
                goto SendUserData;
            }
            // invalide data
            return ;
        }
        Shell.Current.GoToAsync($"/{nameof(MainPage)}"); // ?UserId = responce.Id
    }


    private bool ValidateData()
    {
        if(PhoneNumber[0] =='+')
        {
            string? skipPlus = PhoneNumber.Skip(1).ToString();
            if(!int.TryParse(PhoneNumber,out int number))
            {
                //
                return false;
            }
        }
        else if(!int.TryParse(PhoneNumber, out int number))
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
}
