namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(_isUpdate), "_isUpdate")]
public partial class EditEquipmentPage : ContentPage
{
    public  bool _isUpdate;
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