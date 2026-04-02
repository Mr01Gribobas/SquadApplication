namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(IsUpdate), "_isUpdate")]
public partial class EditEquipmentPage : ContentPage
{
    private  bool _isUpdate {  get; set; }

    public bool IsUpdate
    {
        get => _isUpdate;
        set=> _isUpdate = value;
    }
    public EditEquipmentViewModel _viewModel;
	private readonly UserModelEntity _user;
    public IHttpClientFactory _httpClientFactory;
	public EditEquipmentPage(IUserSession userSession,IHttpClientFactory clientFactory)
	{
		_user = userSession.CurrentUser;
        _httpClientFactory = clientFactory;
        _viewModel = new EditEquipmentViewModel(this,_user);
		BindingContext = _viewModel;
		InitializeComponent();
	}
}