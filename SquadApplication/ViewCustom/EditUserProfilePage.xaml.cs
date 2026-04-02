namespace SquadApplication.ViewCustom;
public partial class EditUserProfilePage : ContentPage
{
    private readonly UserModelEntity _user;
    private readonly EditYourProfileViewModel _editProfileViewModel;
    public IHttpClientFactory _clientFactory { get; private set; }
    public EditUserProfilePage(IUserSession userSession,IHttpClientFactory clientFactory)
	{
		_user = userSession.CurrentUser;
        _clientFactory = clientFactory;
		_editProfileViewModel = new EditYourProfileViewModel(this,userSession.CurrentUser);
        BindingContext = _editProfileViewModel;
		InitializeComponent();
    }
}