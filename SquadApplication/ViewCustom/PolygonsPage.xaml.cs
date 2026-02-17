namespace SquadApplication.ViewCustom;

public partial class PolygonsPage : ContentPage
{
    public PolygonsViewModel _polygonsViewModel;
    private UserModelEntity? _user;
    public PolygonsPage(IUserSession userSession)
    {
        _user = userSession.CurrentUser;
        if(_user is null)
        {
            //TODO
        }
        InitializeComponent();
        _polygonsViewModel = new PolygonsViewModel(this, _user);
        BindingContext = _polygonsViewModel;
        Loaded += PolygonsPage_Loaded;
    }

    private void PolygonsPage_Loaded(object? sender, EventArgs e)
    {
        CheckItems();
    }

    private void CheckItems()
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

    }

}

