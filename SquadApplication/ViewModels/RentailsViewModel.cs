using Microsoft.Maui.Controls.Shapes;

namespace SquadApplication.ViewModels;
public partial class RentailsViewModel : ObservableObject
{
    private readonly UserModelEntity _user;
    private readonly RentalsPage _rentalPage;
    private readonly IRequestManager<RentailsViewModel> _requestManager;
    public Int32 _countPolygon => Rentals.Count;

    [ObservableProperty]
    private ObservableCollection<ReantalEntity> rentals;



    public RentailsViewModel(RentalsPage rentalsPage , UserModelEntity modelEntity)
    {
        _user = modelEntity;
        _rentalPage = rentalsPage;
        _requestManager = new ManagerGetRequests<RentailsViewModel>();
        GetRentalsFromDb();
    }

    private void GetRentalsFromDb()
    {
        var request = (ManagerGetRequests<RentailsViewModel>)_requestManager;
        request.SetUrl("");
    }
}
