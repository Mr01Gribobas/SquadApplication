using SquadApplication.ViewModels;

namespace SquadApplication.ViewCustom;

public partial class FeesPage : ContentPage
{
	public FeesPage()
	{
		InitializeComponent();
		BindingContext = new FeesViewModel();

    }
}