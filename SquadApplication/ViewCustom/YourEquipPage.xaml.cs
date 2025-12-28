namespace SquadApplication.ViewCustom;
public partial class YourEquipPage : ContentPage
{
	private YourEquipViewModel _viewModel;
	
	public YourEquipPage(IUserSession userSession)
	{
		InitializeComponent();
		_viewModel = new YourEquipViewModel(this,userSession.CurrentUser);
		BindingContext = _viewModel;

	}
}