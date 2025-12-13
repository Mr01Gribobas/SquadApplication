
namespace SquadApplication.ViewCustom;

public partial class RentalsPage : ContentPage
{
	public RentalsPage(IUserSession userSession)
	{
		InitializeComponent();
        BindingContext = new RentailsViewModel();

    }
}