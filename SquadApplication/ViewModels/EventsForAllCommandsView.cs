using SquadApplication.ClassView;
using SquadApplication.Repositories.ManagerRequest.UpgradeRequestManager;

namespace SquadApplication.ViewModels;

public partial class EventsForAllCommandsView : ObservableObject
{
    private readonly EventsForAllCommandsPage _page;
    private readonly IUserSession _user;
    private readonly BaseRequestsManager _requestManager;

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
        events = new ObservableCollection<EventsForAllCommandsModelDTO>();
        _requestManager = new BaseRequestsManager(_page._clientFactory.CreateClient());
        this.FilterByDate();
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
    }

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
                while((j >= step) && events[j - step].Date > events[j].Date)
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
            return;
        await Shell.Current.GoToAsync($"/{nameof(CreateEventsForAllCommandsPage)}/?CommanderId={_user.CurrentUser.Id}");
        //GetEventsAsync();
    }
    [RelayCommand]
    public async void EditEvent(EventsForAllCommandsModelDTO model)
    {
        try
        {
            string? curentTeamUser = _user.CurrentUser._teamName is null ? throw new NullReferenceException("У вас нет команды  !!! А вы еще и пытаетесь изменить событие чужой команды ") : _user.CurrentUser._teamName;
            if(_user.CurrentUser._role != Role.Commander || _user.CurrentUser is null || model.TeamNameOrganization != curentTeamUser)
                throw new Exception("У вас нет прав на изменение данного событияя ");
            _page._cache.Set<EventsForAllCommandsModelDTO>("EventForCommands", model);
            await Shell.Current.GoToAsync($"/{nameof(CreateEventsForAllCommandsPage)}/?CommanderId={_user.CurrentUser.Id}");
        }
        catch(Exception ex)
        {
            await _page.DisplayAlertAsync("Error", $"{ex.Message}", "Ok");
            return;
        }

    }

    [RelayCommand]
    public async void UserIsGoRheGame(EventsForAllCommandsModelDTO model)
    {
        SendRequest(model, true);
    }

    private async Task SendRequest(EventsForAllCommandsModelDTO model, bool isGoing)
    {

        _requestManager.SetAddress($"api/events/AppendOrDeleteFromTheMeeting?nameTeamOrganization={model.TeamNameOrganization}&userId={_user.CurrentUser.Id}&turnout={isGoing}");
        var result = await _requestManager.PatchDateAsync<EventsForAllCommandsModelDTO>(null);
        _requestManager.ResetAddress();
    }

    [RelayCommand]
    public async void UserIsNotGoRheGame(EventsForAllCommandsModelDTO model)
    {
        SendRequest(model, false);
    }
    public async Task GetEventsAsync()
    {
        _requestManager.SetAddress("api/events/AllEventsForCommands");
        List<EventsForAllCommandsModelDTO>? responce = await _requestManager.GetDateAsync<List<EventsForAllCommandsModelDTO>>();
        if(responce is not null && responce.Count > 0)
        {
            foreach(EventsForAllCommandsModelDTO item in responce)
                Events.Add(item);
        }
        _requestManager.ResetAddress();
    }
}
