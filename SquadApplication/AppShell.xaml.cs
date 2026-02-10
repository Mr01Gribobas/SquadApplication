namespace SquadApplication;

public partial class AppShell : Shell
{
    private readonly IServiceProvider _serviceProvider;
    public AppShell(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();
        InitRouting();
    }

   
    private void InitRouting()
    {
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(ParticipantsPage), typeof(ParticipantsPage));
        Routing.RegisterRoute(nameof(RentalsPage), typeof(RentalsPage));
        Routing.RegisterRoute(nameof(OrderPage), typeof(OrderPage));

        Routing.RegisterRoute(nameof(FeesPage), typeof(FeesPage));
        Routing.RegisterRoute(nameof(CreateEventPage), typeof(CreateEventPage));
        Routing.RegisterRoute(nameof(EventsForAllCommandsPage), typeof(EventsForAllCommandsPage));
        //Routing.RegisterRoute(nameof(CreateEventsForAllCommandsPage), typeof(CreateEventsForAllCommandsPage));

        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(EditEquipmentPage), typeof(EditEquipmentPage));
        Routing.RegisterRoute(nameof(EditUserProfilePage), typeof(EditUserProfilePage));

        Routing.RegisterRoute(nameof(PolygonsPage), typeof(PolygonsPage));
        Routing.RegisterRoute(nameof(AppendPolygonPage), typeof(AppendPolygonPage));



    }
}



