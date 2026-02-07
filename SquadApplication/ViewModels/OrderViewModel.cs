namespace SquadApplication.ViewModels;
public partial class OrderViewModel : ObservableObject
{
    private OrderPage _orderPage;
   
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string price;

    [ObservableProperty]
    private string count = "0";

    [ObservableProperty]
    private ObservableCollection<Order> orders;

    [ObservableProperty]
    private string totalSum;


    public OrderViewModel(OrderPage orderPage)
    {
        _orderPage = orderPage;
        orders = new ObservableCollection<Order>();
    }

    [RelayCommand]
    public void AddProduct()
    {
        if(int.TryParse(Count, out int newValue))
        {
            newValue++;
            Count = newValue.ToString();

        }
        else
        {
            _orderPage.DisplayAlertAsync("info", "errorFormat", "Ok");
        }

    }
    [RelayCommand]
    public void ReduceProduct()
    {
        if(int.TryParse(Count, out int newValue))
        {
            newValue--;
            Count = newValue.ToString();

        }
        else
        {
            _orderPage.DisplayAlertAsync("info", "errorFormat", "Ok");
        }
    }

    [RelayCommand]
    public void AddInOrder()
    {
        try
        {
            Order newOrder = new Order(Name, int.Parse(Price), int.Parse(Count));
            Orders.Add(newOrder);
            UpdateTotalSum();

        }
        catch(Exception)
        {

            _orderPage.DisplayAlertAsync("info", "errorFormat", "Ok");

        }
    }

    private void UpdateTotalSum()
    {
        var sum = Orders.Select(p => p._totalSum).Sum();
        TotalSum = $"Общая сумма : {sum}";
    }

    [RelayCommand]
    public void ResetList()
    {
        UpdateTotalSum();
        Orders.Clear();

    }
}