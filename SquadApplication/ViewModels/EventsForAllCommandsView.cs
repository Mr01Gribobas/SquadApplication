using SquadApplication.ClassView;

namespace SquadApplication.ViewModels;

public partial class EventsForAllCommandsView : ObservableObject
{
    private readonly EventsForAllCommandsPage _page;
    private readonly IUserSession _user;
    private readonly ManagerGetRequests<EventsForAllCommandsModelDTO> _getRequestMansger;
    
    [ObservableProperty]
    private FilterType filterType ;

    [ObservableProperty]
    private string searchByNameGame;

    [ObservableProperty]
    private ObservableCollection<EventsForAllCommandsModelDTO> events;

    public EventsForAllCommandsView(EventsForAllCommandsPage page,IUserSession user)
    {
        _page = page;
        _user = user;
        _getRequestMansger = new ManagerGetRequests<EventsForAllCommandsModelDTO>();
        events = new ObservableCollection<EventsForAllCommandsModelDTO>();
        this.FilterType =  FilterType.ByDate;
    }


    [RelayCommand]
    public async void CreateEvent()
    {
        Console.WriteLine("Create");
        if(_user.CurrentUser._role != Role.Commander)
        {
            return;
        }
        await Shell.Current.GoToAsync($"/{nameof(CreateEventsForAllCommandsPage)}/?CommanderId={_user.UserId}");
        GetEventsAsync();
    }

    public async Task GetEventsAsync()
    {
        _getRequestMansger.SetUrl("GetAllEventsForAllCommands");
        List<EventsForAllCommandsModelDTO> responce = await _getRequestMansger.GetDataAsync(GetRequests.EventsForCommands);
        if(responce is not null && responce.Count > 0)
        {
            foreach(EventsForAllCommandsModelDTO item in responce)
            {
                Events.Add(item);
            }
        }
        _getRequestMansger.ResetUrlAndStatusCode();
    }
}
