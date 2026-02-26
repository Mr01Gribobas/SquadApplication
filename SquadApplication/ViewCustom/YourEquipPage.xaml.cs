namespace SquadApplication.ViewCustom;
public partial class HomePage : ContentPage
{
	private HomeViewModel _viewModel;
	
	public HomePage(IUserSession userSession)
	{
		_viewModel = new HomeViewModel(this,userSession.CurrentUser);
		BindingContext = _viewModel;
		InitializeComponent();

	}
}