namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(_refreshPage), "refresh")]
public partial class PolygonsPage : ContentPage
{
    public PolygonsViewModel _polygonsViewModel;
    private UserModelEntity? _user;
    public IHttpClientFactory _clientFactory { get; private set; }
    public bool _refreshPage
    {
        set
        {
            if(value)
                RefreshData();
        }
    }


    public PolygonsPage(IUserSession userSession, IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _user = userSession.CurrentUser;
        _polygonsViewModel = new PolygonsViewModel(this, _user);
        BindingContext = _polygonsViewModel;
        InitializeComponent();
    }
    private void RefreshData()=> _polygonsViewModel?.SetPolygons();
    
    public async Task CheckItems()
    {
        if(_polygonsViewModel._countPolygon <= 0)
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
