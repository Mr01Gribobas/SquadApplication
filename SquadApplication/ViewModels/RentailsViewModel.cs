using Microsoft.Maui.Controls.Shapes;
using SquadApplication.DTO_Classes.DTO_AuxiliaryModels;
using SquadApplication.Repositories.ManagerRequest.Interfaces;

namespace SquadApplication.ViewModels;
public partial class RentailsViewModel : ObservableObject
{
    private readonly UserModelEntity _user;
    private readonly RentalsPage _rentalPage;
    private readonly IRequestManager<RentailsDTO> _requestManager;
    public Int32 _countPolygon => Rentals.Count;

    [ObservableProperty]
    private ObservableCollection<RentailsDTO> rentals;


   






    public RentailsViewModel(RentalsPage rentalsPage , UserModelEntity modelEntity)
    {
        _user = modelEntity;
        _rentalPage = rentalsPage;
        _requestManager = new ManagerGetRequests<RentailsDTO>();
        GetRentalsFromDb();
    }


    [RelayCommand]
    public void DeleteRental()
    {

    }


    private void GetRentalsFromDb()
    {
        var request = (ManagerGetRequests<RentailsDTO>)_requestManager;
        request.SetUrl("");
    }
}
