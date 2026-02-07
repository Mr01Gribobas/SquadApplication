namespace SquadApplication.ViewCustom;

public partial class RentalsPage : ContentPage
{
    private readonly UserModelEntity _user;
    private readonly RentailsViewModel _rentalView;

    public RentalsPage(IUserSession userSession)
    {
        _user = userSession.CurrentUser;
        _rentalView = new RentailsViewModel(this,_user);
        BindingContext = _rentalView;
        InitializeComponent();
    }
    private void CheckItems()
    {
        if(_rentalView._countPolygon <= 0)
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