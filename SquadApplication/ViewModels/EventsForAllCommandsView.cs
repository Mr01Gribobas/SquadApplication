using SquadApplication.ClassView;

namespace SquadApplication.ViewModels;

public partial class EventsForAllCommandsView : ObservableObject
{
    private readonly EventsForAllCommandsPage _page;
    private readonly IUserSession _user;
    private readonly ManagerGetRequests<EventsForAllCommandsModelDTO> _getRequestMansger;

    [ObservableProperty]
    private FilterType filterType;

    [ObservableProperty]
    private string searchByNameGame;

    [ObservableProperty]
    private ObservableCollection<EventsForAllCommandsModelDTO> events;

    public EventsForAllCommandsView(EventsForAllCommandsPage page, IUserSession user)
    {
        _page = page;
        _user = user;
        _getRequestMansger = new ManagerGetRequests<EventsForAllCommandsModelDTO>();
        events = new ObservableCollection<EventsForAllCommandsModelDTO>();
        this.FilterType = FilterType.ByDate;
    }





    [RelayCommand]
    public void FilterByDate()
    {
        this.FilterType = FilterType.ByDate;
        SortEvents(FilterType.ByDate);
    }
    [RelayCommand]
    public void FilterMineFirst()
    {
        this.FilterType = FilterType.MyEvent;
        SortEvents(FilterType.MyEvent);

    }
    [RelayCommand]
    public void FilterFirstOldEvent()
    {
        this.FilterType = FilterType.OldEvent;
        SortEvents(FilterType.OldEvent);
    }


    private void SortEvents(FilterType filter)
    {
        if(Events is null || Events.Count <= 0)
            return;
        switch(filter)
        {
            case FilterType.MyEvent:
                MineMySort(Events);
                break;
            case FilterType.OldEvent:
                SortByOldEvent(Events);
                break;
            case FilterType.ByDate:
                SortByDate(Events);
                break;
        }
    }

    private void SortByOldEvent(ObservableCollection<EventsForAllCommandsModelDTO> events)
    {
        int left = 0;
        for(int i = 0; i < events.Count; i++)
        {
            if(events[left].Date > DateOnly.FromDateTime(DateTime.Now))
            {
                for(int j = left + 1; j < events.Count; j++)
                {
                    if(events[j].Date < DateOnly.FromDateTime(DateTime.Now))
                    {
                        SwopItemsByIndex(events, j, left);
                        left++;
                    }
                }
            }
            else
                left++;
        }
    }//todo algoritm


    private void MineMySort(ObservableCollection<EventsForAllCommandsModelDTO> events)
    {
        int left = 0;
        string myTeamName = _user.CurrentUser._teamName;
        for(int i = 0; i < events.Count; i++)
        {
            if(events[left].TeamNameOrganization != myTeamName)
            {
                for(int j = left + 1; j < events.Count; j++)
                {
                    if(events[j].TeamNameOrganization == myTeamName)
                    {
                        SwopItemsByIndex(events, left, j);
                        left++;
                        break;
                    }
                }
            }
            else
                left++;
        }
    }

    private void SortByDate(ObservableCollection<EventsForAllCommandsModelDTO> events)
    {
        var step = events.Count / 2;
        while(step > 0)
        {
            for(int i = step; i < events.Count; i++)
            {
                int j = i;
                while((j >= step) && events[j - step].Date < events[j].Date)
                {
                    SwopItemsByIndex(events, (j - step), j);
                    j -= step;
                }
            }
            step /= 2;
        }
    }

    private void SwopItemsByIndex(ObservableCollection<EventsForAllCommandsModelDTO> events, int item1, int item2)
    {
        var temp = events[item1];
        events[item1] = events[item2];
        events[item2] = temp;
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
