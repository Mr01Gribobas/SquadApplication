
namespace SquadApplication.ViewCustom;

public partial class RentalsPage : ContentPage
{
	public RentalsPage()
	{
		InitializeComponent();
        BindingContext = new RentailsViewModel();

    }
}