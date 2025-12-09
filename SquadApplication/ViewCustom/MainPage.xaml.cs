using SquadApplication.AnimationCustom;
using SquadApplication.Repositories.ManagerRequest;
using System.Threading.Tasks;

namespace SquadApplication.ViewCustom;


[QueryProperty(nameof(UserId), "UserId")]
public partial class MainPage : ContentPage
{
    private UserModelEntity User { get; set; }
    public int UserId
    {
        get => UserId;
        set
        {
            User = GetUser(value).Result;
        }
    }

    private async Task<UserModelEntity> GetUser(int value)
    {
        var user =  await ManagerGetRequests<UserModelEntity>.GetUserById(value);
        if (user == null)
        {
            //error
        }
        return user;
    }

    public MainPage()
    {
        InitializeComponent();
        BindingContext = new MainViewModel();

    }

    private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var result = e?.CurrentSelection.FirstOrDefault();
        if(result != null)
        {
            await GoNextPage(result);
            ListItems.SelectedItem = null;
        }
    }

    //private CustomsAnimation customsAnimation = new CustomsAnimation();
    //private async Task AnimateTest()
    //{
    //    await ListItems.RotateToAsync(360, 1000);
    //}



    private static async Task GoNextPage(object? result)
    {
        switch(result.ToString())
        {
            case "Главная":
                await Shell.Current.GoToAsync($"/{nameof(YourEquipPage)}");// ?UserId={UserId}
                break;
            case "Сборы":
                await Shell.Current.GoToAsync($"/{nameof(FeesPage)}");// ?UserId={UserId}
                break;
            case "Участники":
                await Shell.Current.GoToAsync($"/{nameof(ParticipantsPage)}");// ?UserId={UserId}
                break;
            case "Прокаты":
                await Shell.Current.GoToAsync($"/{nameof(RentalsPage)}");// ?UserId={UserId}
                break;
            case "Профиль":
                await Shell.Current.GoToAsync($"/{nameof(ProfilePage)}");// ?UserId={UserId}
                break;
            case "Заказы":
                await Shell.Current.GoToAsync($"/{nameof(OrderPage)}");
                break;
            case "Полигоны":
                await Shell.Current.GoToAsync($"/{nameof(PolygonsPage)}");
                break;
            default:
                break;
        }
    }
}