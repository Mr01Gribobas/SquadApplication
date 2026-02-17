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

    [RelayCommand]
    public async Task CreateRental()
    {
        await Shell.Current.GoToAsync($"/{nameof(CreateOrUpdateRentalPage)}/?IsUpdate={false}");
    }

    [RelayCommand]
    public async Task UpdateRental(RentailsDTO rentail)
    {
        _rentalPage.SaveInCacheItem(rentail);
        await Shell.Current.GoToAsync($"/{nameof(CreateOrUpdateRentalPage)}/?IsUpdate={true}");
    }

    

    private void GetRentalsFromDb()
    {
        var request = (ManagerGetRequests<RentailsDTO>)_requestManager;
        request.SetUrl("");
    }
}
