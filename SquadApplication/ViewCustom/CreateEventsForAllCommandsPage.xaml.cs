namespace SquadApplication.ViewCustom;


[QueryProperty(nameof(CommanderId), "CommanderId")]
public partial class CreateEventsForAllCommandsPage : ContentPage
{
    public readonly ICacheServieseCust _cache;
    private readonly IUserSession _user;
    private readonly CreateEventsForAllCommandsViewModel _createEventsForAllCommandsView;
    public int CommanderId;

    public CreateEventsForAllCommandsPage(IUserSession user, ICacheServieseCust cache)
    {
        _cache = cache;
        _user = user;
        _createEventsForAllCommandsView = new CreateEventsForAllCommandsViewModel(this, user, false);
        BindingContext = _createEventsForAllCommandsView;
        InitializeComponent();
    }
}