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
        Loaded += ProfilePage_Loaded;
    }

    private void ProfilePage_Loaded(object? sender, EventArgs e)
    {
        _homePageView.GetFullInfoForProfile(_user.Id);
    }
}