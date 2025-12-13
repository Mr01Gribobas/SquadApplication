namespace SquadApplication.ViewCustom;

public partial class ProfilePage : ContentPage
{
	private UserModelEntity _user;
	public ProfilePage(IUserSession userSession)
	{
		_user = userSession.CurrentUser; 
		InitializeComponent();
        BindingContext = new ProfileViewModel(_user);

    }
}