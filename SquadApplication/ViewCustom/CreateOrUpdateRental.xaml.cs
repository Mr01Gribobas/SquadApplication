namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(_isUpdate), "IsUpdate")]
public partial class CreateOrUpdateRentalPage : ContentPage
{
    private readonly ICacheServieseCust _cacheServiese;
    private readonly IUserSession _user;
    public readonly CreateOrUpdateRentalViewModel _createOrUpdateRentalViewModel;
    public bool _isUpdate { get; set; }

    public CreateOrUpdateRentalPage(IUserSession user, ICacheServieseCust cacheServiese)
    {
        _cacheServiese = cacheServiese;
        _user = user;
        _createOrUpdateRentalViewModel = new CreateOrUpdateRentalViewModel(this, user);
        BindingContext = _createOrUpdateRentalViewModel;
        InitializeComponent();
        Loaded += CreateOrUpdateRental_Loaded;
    }

    private void CreateOrUpdateRental_Loaded(object? sender, EventArgs e)
    {
        if(!_isUpdate)
        {
            _createOrUpdateRentalViewModel.NumderRental = "-";
            return;
        }

        var resultItem = _cacheServiese.GetItemByKey<RentailsDTO>("updateRental");
        if(resultItem is not null)
            _createOrUpdateRentalViewModel.InitialProperty(resultItem);
        _cacheServiese.Remove("updateRental");
    }
}