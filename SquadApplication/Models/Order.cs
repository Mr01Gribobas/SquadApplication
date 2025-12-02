namespace SquadApplication.Models;

public class Order
{
    public Order(string name, int price,int count )
    {
        _nameProduct = name;
        _priceProduct = price;
        _countProduct = count;
        _totalSum = TotalSumProducts(price,count);
    }
    public string _nameProduct { get; set; }
    public int _priceProduct { get; set; }
    public int _countProduct { get; set; }
    public int _totalSum { get; set; }


    private int TotalSumProducts(int price, int count)
    {
        return price * count;
    }
}
