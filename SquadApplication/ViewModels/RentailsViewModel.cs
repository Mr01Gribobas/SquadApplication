namespace SquadApplication.ViewModels;

public partial class RentailsViewModel : ObservableObject
{
    private readonly UserModelEntity _user;
    private readonly RentalsPage _rentalPage;
    private readonly IRequestManager<RentailsDTO> _requestManager;
    public Int32 _countRentals => Rentals.Count;


    [ObservableProperty]
    private ObservableCollection<RentalDTOToString> rentals;









    public RentailsViewModel(RentalsPage rentalsPage, UserModelEntity modelEntity)
    {
        _user = modelEntity;
        _rentalPage = rentalsPage;
        rentals = new ObservableCollection<RentalDTOToString>();
        _requestManager = new ManagerGetRequests<RentailsDTO>();
        GetRentalsFromDb();
    }

    [RelayCommand]
    public async Task CreateRental()
    {
        await Shell.Current.GoToAsync($"/{nameof(CreateOrUpdateRentalPage)}/?IsUpdate={false}");
    }

    [RelayCommand]
    public async Task UpdateRental(RentalDTOToString rentail)
    {
        var rentalConvert = RentalDTOToString.ConvertToRentailsDTO(rentail);
        _rentalPage.SaveInCacheItem(rentalConvert);
        await Shell.Current.GoToAsync($"/{nameof(CreateOrUpdateRentalPage)}/?IsUpdate={true}");
    }




    public async Task GetRentalsFromDb()
    {
        _requestManager.SetUrl($"GetAllReantil?teamId={_user.TeamId}");
        List<RentailsDTO>? resultList = await _requestManager.GetDataAsync(GetRequests.GetAllReantil);
        if(resultList is not null && resultList.Count > 0)
        {
            foreach(RentailsDTO item in resultList)
            {
                var modelRental = RentalDTOToString.ConvertToRentailsDTOString(item);
                Rentals.Add(modelRental);
            }

        }
        _requestManager.ResetUrlAndStatusCode();
        _rentalPage.CheckItems();
    }





}
