
namespace SquadApplication.ViewCustom;

public partial class RentalsPage : ContentPage
{
    private readonly UserModelEntity _user;
    private readonly RentailsViewModel _rentalView;

    public RentalsPage(IUserSession userSession)
    {
        _user = userSession.CurrentUser;
        _rentalView = new RentailsViewModel();
        BindingContext = _rentalView;
        InitializeComponent();


    }
}