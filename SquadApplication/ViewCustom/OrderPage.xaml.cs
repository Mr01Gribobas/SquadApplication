namespace SquadApplication.ViewCustom;

public partial class OrderPage : ContentPage
{
	private OrderViewModel _orderViewModel;
    public OrderPage()
	{
        _orderViewModel= new OrderViewModel(this);
        BindingContext = _orderViewModel;
		InitializeComponent();
    }
}