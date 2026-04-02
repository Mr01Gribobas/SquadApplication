namespace SquadApplication.ViewCustom;


[QueryProperty(nameof(CommanderId), "CommanderId")]
public partial class CreateEventsForAllCommandsPage : ContentPage
{
    public readonly ICacheServieseCust _cache;
    private readonly IUserSession _user;
    private readonly CreateEventsForAllCommandsViewModel _createEventsForAllCommandsView;
    public IHttpClientFactory _clientFactory {  get; private set; }
    public int CommanderId;

    public CreateEventsForAllCommandsPage(IUserSession user, ICacheServieseCust cache,IHttpClientFactory clientFactory)
    {

        _cache = cache;
        _user = user;
        _clientFactory = clientFactory;
        _createEventsForAllCommandsView = new CreateEventsForAllCommandsViewModel(this, user);
        BindingContext = _createEventsForAllCommandsView;
        InitializeComponent();
    }
}