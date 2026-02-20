using SquadApplication.DTO_Classes.DTO_AuxiliaryModels;
using SquadApplication.Services.CacheServiseDir;

namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(_isUpdate), "IsUpdate")]
public partial class CreateOrUpdateRentalPage : ContentPage
{
    private readonly ICacheServieseCust _cacheServiese;
    private readonly IUserSession _user;
    public readonly CreateOrUpdateRentalViewModel _createOrUpdateRentalViewModel;
    private bool _isUpdate;

    public CreateOrUpdateRentalPage(IUserSession user,ICacheServieseCust cacheServiese)
	{
        _cacheServiese = cacheServiese;
        _user = user;
        _createOrUpdateRentalViewModel = new CreateOrUpdateRentalViewModel(this,user,_isUpdate);
        BindingContext = _createOrUpdateRentalViewModel;
        InitializeComponent();
        Loaded += CreateOrUpdateRental_Loaded;
	}

    private void CreateOrUpdateRental_Loaded(object? sender, EventArgs e)
    {
        if(!_isUpdate)
            return;
        var resultItem = _cacheServiese.GetItemByKey<RentailsDTO>("updateRental");
        if(resultItem is not null)
        {
            _createOrUpdateRentalViewModel.InitialProperty(resultItem);
        }
        _cacheServiese.Remove("updateRental");
    }
}