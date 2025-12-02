using SquadApplication.ViewModels;

namespace SquadApplication.ViewCustom;

public partial class FeesPage : ContentPage
{
	private FeesViewModel _feesViewModel;
    public FeesPage()
	{
		InitializeComponent();
		_feesViewModel = new FeesViewModel();
		BindingContext = _feesViewModel;

    }
}