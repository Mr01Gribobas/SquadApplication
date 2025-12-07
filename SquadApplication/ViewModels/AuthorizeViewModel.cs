using SquadApplication.Repositories;
using SquadApplication.Repositories.Interfaces;

namespace SquadApplication.ViewModels;


public partial class AuthorizeViewModel  : ObservableObject
{
    private AuthorizedPage _authorizedPage;
    private IRequestManager _requestManager;
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
        Shell.Current.GoToAsync($"/{nameof(MainPage)}");
    }


    [RelayCommand]
    private void Registration()
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
        DataBaseManager requestManager = (DataBaseManager)_requestManager;
        UserModelEntity responce = requestManager.SendDataForRegistration(newUser).Result;
        if(responce is null)
        {
            if(requestManager.GetStatusCode()== 201)
            {
                
            }
            return ;
        }
        Shell.Current.GoToAsync($"/{nameof(MainPage)}");

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
