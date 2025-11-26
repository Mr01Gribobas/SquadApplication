using SquadApplication.ViewModels;
namespace SquadApplication.ViewCustom;

public partial class MainPage : ContentPage
{
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


    private static async Task GoNextPage(object? result)
    {
        switch(result.ToString())
        {
            case "Главная":
                await Shell.Current.GoToAsync($"/{nameof(YourEquipPage)}");
                break;
            case "Сборы":
                await Shell.Current.GoToAsync($"/{nameof(FeesPage)}");
                break;
            case "Участники":
                await Shell.Current.GoToAsync($"/{nameof(ParticipantsPage)}");
                break;
            case "Полигоны":
                await Shell.Current.GoToAsync($"/{nameof(PolygonsPage)}");
                break;
            case "Прокаты":
                await Shell.Current.GoToAsync($"/{nameof(RentalsPage)}");
                break;
            case "Профиль":
                await Shell.Current.GoToAsync($"/{nameof(ProfilePage)}");
                break;
            case "Заказы":
                await Shell.Current.GoToAsync($"/{nameof(ProfilePage)}");
                break;
            default:
                break;
        }
    }
}