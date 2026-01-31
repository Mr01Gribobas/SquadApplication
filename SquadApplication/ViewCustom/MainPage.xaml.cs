using SquadApplication.Services.NotificationService;

namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(UserId), "UserId")]
public partial class MainPage : ContentPage
{
    private IUserSession _userSession1;
    private readonly NotificationLocalService _notificationLocal;
    private IDispatcherTimer _timer;
    private int sum = 0;
    public MainPage(IUserSession userSession)
    {
        InitializeComponent();
        BindingContext = new MainViewModel();
        _userSession1 = userSession;
        _notificationLocal = new NotificationLocalService();
        StartCheckNotification();
    }

    private async Task StartCheckNotification()
    {
        if(_userSession1 is null || _userSession1.CurrentUser is null)
        {
            return;
        }
        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(10);
        _timer.Tick += async (sender, e) =>
        {
            await _notificationLocal.CheckForEventNotification((int)_userSession1.CurrentUser.TeamId);
            sum += 1;
        };
        _timer.Start();
    }




    private UserModelEntity User { get; set; }
    public int UserId
    {
        get => User.Id;
        set
        {
            GetAndSetUserAsync(value);
        }
    }
    private async Task GetAndSetUserAsync(int value)
    {
        UserModelEntity? user = await ManagerGetRequests<UserModelEntity>.GetUserById(value);
        if(user == null)
        {
            await Shell.Current.GoToAsync($"..");

        }
        _userSession1.CurrentUser = user;
        User = user;
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



    private async Task GoNextPage(object? result)
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
            case "Прокаты":
                await Shell.Current.GoToAsync($"/{nameof(RentalsPage)}");
                break;
            case "Профиль":
                await Shell.Current.GoToAsync($"/{nameof(ProfilePage)}");
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