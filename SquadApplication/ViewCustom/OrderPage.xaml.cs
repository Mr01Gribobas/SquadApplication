using SquadApplication.ViewModels;

namespace SquadApplication.ViewCustom;

public partial class OrderPage : ContentPage
{
	public OrderPage()
	{
		InitializeComponent();
		BindingContext = new OrderViewModel();

    }
}