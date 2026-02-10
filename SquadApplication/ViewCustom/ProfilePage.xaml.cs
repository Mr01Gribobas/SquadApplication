namespace SquadApplication.ViewCustom;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileViewModel _homePageView;
    private UserModelEntity _user;
	public ProfilePage(IUserSession userSession)
	{
		_user = userSession.CurrentUser; 
		InitializeComponent();
        _homePageView = new ProfileViewModel(this,_user);
        BindingContext = _homePageView;
        
    }
}