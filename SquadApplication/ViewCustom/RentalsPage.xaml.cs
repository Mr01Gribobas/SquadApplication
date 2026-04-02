
namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(_refreshPage), "refresh")]
public partial class RentalsPage : ContentPage
{
    private readonly ICacheServieseCust _cacheServiese;
    private readonly UserModelEntity _user;
    private readonly RentailsViewModel _rentalView;
    public IHttpClientFactory _clientFactory { get; private set; }
    public bool _refreshPage
    {
        set
        {
            if(value is true)
                RefreshData();
        }
    }



    public RentalsPage(IUserSession userSession, ICacheServieseCust cacheServiese, IHttpClientFactory clientFactory)
    {
        _user = userSession.CurrentUser;
        _clientFactory = clientFactory;
        _cacheServiese = cacheServiese;
        _rentalView = new RentailsViewModel(this, _user);
        BindingContext = _rentalView;
        InitializeComponent();
    }
    private async Task RefreshData() => await _rentalView.GetRentalsFromDb();

    public void SaveInCacheItem(RentailsDTO rentail) => _cacheServiese.Set<RentailsDTO>("updateRental", rentail);

    public void CheckItems()
    {
        if(_rentalView._countRentals <= 0)
        {
            loyoutItem.Add(
                new Label()
                {
                    BackgroundColor = Colors.Transparent,//"#50508080",
                    TextColor = Colors.DarkGray,
                    FontSize = 30,
                    Text = "Not found !",
                    Margin = new Thickness(0, 200, 0, 0),
                    HorizontalOptions = LayoutOptions.Center
                }
                );
        }
        else
        {
            foreach(var item in loyoutItem)
            {
                var label = item as Label;
                if(label != null && label.Text == "Not found !")
                    loyoutItem.Remove(item);
            }
        }

    }
}