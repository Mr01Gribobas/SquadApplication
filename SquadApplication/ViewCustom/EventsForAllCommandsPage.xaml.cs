
namespace SquadApplication.ViewCustom;

[QueryProperty(nameof(_refreshPage), "refresh")]
public partial class EventsForAllCommandsPage : ContentPage
{
    public readonly ICacheServieseCust _cache;
    private readonly IUserSession _user;
    private readonly EventsForAllCommandsView _eventsForAllCommandsView;
    public IHttpClientFactory _clientFactory { get; private set; }
    private bool _refreshPage
    {
        set
        {
            if(value)
                RefreshData();
        }
    }

    

    public EventsForAllCommandsPage(IUserSession user, ICacheServieseCust cache, IHttpClientFactory clientFactory)
    {
        _cache = cache;
        _user = user;
        _clientFactory = clientFactory;
        _eventsForAllCommandsView = new EventsForAllCommandsView(this, user);
        BindingContext = _eventsForAllCommandsView;
        InitializeComponent();
        Loaded += EventsForAllCommandsPage_Loaded;
    }
    private async Task RefreshData() => await _eventsForAllCommandsView.GetEventsAsync();
    private async void EventsForAllCommandsPage_Loaded(object? sender, EventArgs e)
    {
       await _eventsForAllCommandsView?.GetEventsAsync();
    }
}