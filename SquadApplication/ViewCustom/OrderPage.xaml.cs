using SquadApplication.ViewModels;

namespace SquadApplication.ViewCustom;

public partial class OrderPage : ContentPage
{
	private OrderViewModel _orderViewModel;
    public OrderPage()
	{
		InitializeComponent();
        _orderViewModel= new OrderViewModel(this);
        BindingContext = _orderViewModel;
		
    }
}