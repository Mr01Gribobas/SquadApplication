namespace SquadApplication.ViewCustom;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _homePageView;
    private UserModelEntity _user;
	public HomePage(IUserSession userSession)
	{
		_user = userSession.CurrentUser; 
		InitializeComponent();
        _homePageView = new HomeViewModel(_user);
        BindingContext = _homePageView;

    }
}