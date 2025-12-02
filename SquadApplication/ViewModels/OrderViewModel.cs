using CommunityToolkit.Mvvm.Input;
using SquadApplication.Models;
using SquadApplication.ViewCustom;
using System.Collections.ObjectModel;

namespace SquadApplication.ViewModels;

public partial class OrderViewModel : ObservableObject
{
    private OrderPage _orderPage;
    public OrderViewModel(OrderPage orderPage)
    {
        _orderPage = orderPage;
        orders = new ObservableCollection<Order>();
    }
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string price;

    [ObservableProperty]
    private string count = "0";

    [ObservableProperty]
    private ObservableCollection<Order> orders;

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

        }
        catch(Exception)
        {

            _orderPage.DisplayAlertAsync("info", "errorFormat", "Ok");

        }
    }


}
