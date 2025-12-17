namespace SquadApplication.ViewCustom;

public partial class EditEquipmentPage : ContentPage
{
	public EditEquipmentViewModel _viewModel;
	private readonly UserModelEntity _user;

	public EditEquipmentPage(IUserSession userSession)
	{
		_user = userSession.CurrentUser;
        _viewModel = new EditEquipmentViewModel(this,_user);
		InitializeComponent();
		BindingContext = _viewModel;
	}
}