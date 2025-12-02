using SquadApplication.ViewModels;

namespace SquadApplication.ViewCustom;

public partial class PolygonsPage : ContentPage
{
	public PolygonsViewModel _polygonsViewModel;
	public PolygonsPage()
	{
		InitializeComponent();
        _polygonsViewModel = new PolygonsViewModel();
		BindingContext = _polygonsViewModel;

    }
}