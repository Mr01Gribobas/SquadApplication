using SquadApplication.ViewModels;

namespace SquadApplication.ViewCustom;

public partial class PolygonsPage : ContentPage
{
	public PolygonsPage()
	{
		InitializeComponent();
        BindingContext = new PolygonsViewModel();

    }
}