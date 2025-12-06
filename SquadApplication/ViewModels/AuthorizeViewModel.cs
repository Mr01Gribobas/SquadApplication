using SquadApplication.Repositories;
using SquadApplication.Repositories.Interfaces;

namespace SquadApplication.ViewModels;


public partial class AuthorizeViewModel  :ObservableObject
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
    private string phuneNumber ;

    [RelayCommand]
    private void Login()
    {
        if(AccesCode.Length <= 0  )
        {
            return;
        }
       _requestManager =  (DataBaseManager)_requestManager;
       _requestManager.SendDataForEnter(AccesCode);
        Shell.Current.GoToAsync($"/{nameof(MainPage)}");
    }


    [RelayCommand]
    private void Registration()
    {
        UserModelEntity.CreateUserEntity(
            _teamName: Team,
            _name: Name,
            _callSing: CallSing,
            _phone: PhuneNumber,
            _age:null,
            _role:Role.Commander,
            _teamId:null

            );
    }

}
