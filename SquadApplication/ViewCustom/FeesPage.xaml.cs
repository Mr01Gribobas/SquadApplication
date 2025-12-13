
namespace SquadApplication.ViewCustom;

public partial class FeesPage : ContentPage
{
	private FeesViewModel _feesViewModel;
    public FeesPage(IUserSession userSession)
	{
		InitializeComponent();
		_feesViewModel = new FeesViewModel();
		BindingContext = _feesViewModel;

    }
}