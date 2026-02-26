namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(IsUpdate), "_isUpdate")]
public partial class EditEquipmentPage : ContentPage
{
    private  bool _isUpdate {  get; set; }

    public bool IsUpdate
    {
        get => _isUpdate;
        set
        {
            _isUpdate = value;
        }
    }

    public EditEquipmentViewModel _viewModel;
	private readonly UserModelEntity _user;

	public EditEquipmentPage(IUserSession userSession)
	{
		_user = userSession.CurrentUser;
		var test = _isUpdate;
        _viewModel = new EditEquipmentViewModel(this,_user);
		BindingContext = _viewModel;
		InitializeComponent();
	}

   
}