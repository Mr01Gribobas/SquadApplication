using SquadApplication.DTO_Classes.DTO_AuxiliaryModels;
using SquadApplication.Services.CacheServiseDir;

namespace SquadApplication.ViewCustom;

public partial class RentalsPage : ContentPage
{
    private readonly ICacheServieseCust _cacheServiese;
    private readonly UserModelEntity _user;
    private readonly RentailsViewModel _rentalView;

    public RentalsPage(IUserSession userSession,ICacheServieseCust cacheServiese)
    {
        _cacheServiese = cacheServiese;
        _user = userSession.CurrentUser;
        _rentalView = new RentailsViewModel(this,_user);
        BindingContext = _rentalView;
        InitializeComponent();
    }

    public void SaveInCacheItem(RentailsDTO rentail)
    {
        _cacheServiese.Set<RentailsDTO>("updateRental",rentail);
    }

    public void CheckItems()
    {
        if(_rentalView._countRentals <= 0)
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