using SquadApplication.ViewCustom;
namespace SquadApplication;



public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        InitRouting();
    }

    private void InitRouting()
    {
        Routing.RegisterRoute(nameof(MainPage),typeof(MainPage));
        Routing.RegisterRoute(nameof(FeesPage),typeof(FeesPage));
        Routing.RegisterRoute(nameof(ParticipantsPage),typeof(ParticipantsPage));
        Routing.RegisterRoute(nameof(PolygonsPage),typeof(PolygonsPage));
        Routing.RegisterRoute(nameof(RentalsPage),typeof(RentalsPage));
        Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(YourEquipPage),typeof(YourEquipPage));
    }
}
        
