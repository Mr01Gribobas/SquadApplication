
namespace SquadApplication.ViewCustom;

public partial class CreateEventPage : ContentPage
{
	public CreateEventViewModel _viewModel { get; set; }
	public CreateEventPage(IUserSession userSession)
	{
		InitializeComponent();
		_viewModel = new CreateEventViewModel();
		BindingContext = _viewModel;


	}
}