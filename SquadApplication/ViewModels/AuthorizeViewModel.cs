namespace SquadApplication.ViewModels;


public partial class AuthorizeViewModel  :ObservableObject
{
    private AuthorizedPage _authorizedPage;
    public AuthorizeViewModel(AuthorizedPage authorizedPage)
    {
        _authorizedPage = authorizedPage;
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
