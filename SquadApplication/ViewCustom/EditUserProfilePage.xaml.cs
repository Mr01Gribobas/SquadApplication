
namespace SquadApplication.ViewCustom;

public partial class EditUserProfilePage : ContentPage
{
    private readonly UserModelEntity _user;
    private readonly EditYourProfileViewModel _editProfileViewModel;

    public EditUserProfilePage(IUserSession userSession)
	{
		_user = userSession.CurrentUser;
		_editProfileViewModel = new EditYourProfileViewModel(this,userSession.CurrentUser);
		InitializeComponent();
        BindingContext = _editProfileViewModel;
        

    }
}