using SquadApplication.Repositories.DeviceManager;

namespace SquadApplication;

public partial class AppShell : Shell
{
    private readonly IServiceProvider _serviceProvider;
    private AuthorizedPage _authorizedPage;
    public AppShell(IServiceProvider serviceProvider)
    {
        HomeShellContent.h
        _serviceProvider = serviceProvider;
        InitializeComponent();
        InitRouting();
        this.Navigated += OnShellNavigated;
    }

    private void OnShellNavigated(object? sender, ShellNavigatedEventArgs e)
    {
        if(e.Current?.Location?.OriginalString?.Contains("AuthorizedPage") == true)
        {
            InitialAuthorizePage();
        }
    }

    private void InitialAuthorizePage()
    {
        if(_authorizedPage != null)
        {
            return;
        }
        //if(HomeShellContent is AuthorizedPage page)
        //{
        //    if(page is not null)
        //    {
        //        var userSession = _serviceProvider.GetService<IUserSession>();
        //        var deviceManager = _serviceProvider.GetService<IDeviceManager>();
        //        page.InitializeWithDependencies(userSession, deviceManager);
        //        _authorizedPage = page;
        //    }
        //}


    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        InitialAuthorizePage();
    }


    private void InitRouting()
    {
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ParticipantsPage), typeof(ParticipantsPage));
        Routing.RegisterRoute(nameof(RentalsPage), typeof(RentalsPage));
        Routing.RegisterRoute(nameof(OrderPage), typeof(OrderPage));

        Routing.RegisterRoute(nameof(FeesPage), typeof(FeesPage));
        Routing.RegisterRoute(nameof(CreateEventPage), typeof(CreateEventPage));

        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(YourEquipPage), typeof(YourEquipPage));
        Routing.RegisterRoute(nameof(EditEquipmentPage), typeof(EditEquipmentPage));
        Routing.RegisterRoute(nameof(EditUserProfilePage), typeof(EditUserProfilePage));

        Routing.RegisterRoute(nameof(PolygonsPage), typeof(PolygonsPage));
        Routing.RegisterRoute(nameof(AppendPolygonPage), typeof(AppendPolygonPage));
    }
}



