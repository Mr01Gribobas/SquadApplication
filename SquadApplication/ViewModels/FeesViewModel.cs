namespace SquadApplication.ViewModels;

public partial class FeesViewModel : ObservableObject
{
    public FeesViewModel(FeesPage feesPage, UserModelEntity user)
    {
        _feesPage = feesPage;
        _user = user;
        _requestManager = new ManagerGetRequests<EventModelEntity>();
        GetCurrentEvent();

    }
    private readonly FeesPage _feesPage;
    private readonly UserModelEntity _user;
    private readonly IRequestManager<EventModelEntity> _requestManager;


    [ObservableProperty]
    private ObservableCollection<UserModelEntity> users = new();

    [ObservableProperty]
    private ObservableCollection<UserModelEntity> usersIsGoToTheGame = new();

    [ObservableProperty]
    private ObservableCollection<UserModelEntity> usersIsNotGoTheGame = new();

    [ObservableProperty]
    private string? nameTeamEnemu;

    [ObservableProperty]
    private string? namePolygon;

    [ObservableProperty]
    private string? coordinatesPolygon;

    [ObservableProperty]
    private string? dateAndTime;

    [ObservableProperty]
    private string? countMembers;

    [RelayCommand]
    public async void CurrentHumanWillBe()
    {

    }
    [RelayCommand]
        public async void CurrentHumanWillNot()
    {

    }
    private async void InitialProperty(EventModelEntity? eventFromDb)
    {
        if(eventFromDb is null)
        {
            return;
        }
        NamePolygon = eventFromDb.NamePolygon;
        CoordinatesPolygon = eventFromDb.Coordinates;
        DateAndTime = ConvertDateAndTime(eventFromDb.Date, eventFromDb.Time);
        CountMembers = eventFromDb.CountMembers.ToString();
        await GetMembersTeam(_user.Id);
    }

    private string? ConvertDateAndTime(DateOnly? date, TimeOnly? time)
    {

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(date?.ToString() ?? "Not found");
        stringBuilder.Append(time?.ToString() ?? "Not found");
        return stringBuilder.ToString();
    }

    [RelayCommand]
    private async void CreateEvent()
    {
        await Shell.Current.GoToAsync($"/{nameof(CreateEventPage)}");
    }

    [RelayCommand]
    private async void CopyCoordinates()
    {
        if(CoordinatesPolygon is null)
        {
            return;
        }
        await Clipboard.Default.SetTextAsync(CoordinatesPolygon);
    }


    private async Task GetCurrentEvent()
    {
        if(_requestManager is null | _user is null)
            return;

        var request = (ManagerGetRequests<EventModelEntity>)_requestManager;
        request.SetUrl($"GetEvent?teamId={_user.TeamId}");
        List<EventModelEntity>? responce = await request.GetDataAsync(GetRequests.GetEvent);

        if(responce is null ||
            responce.Count <= 0 ||
            responce.FirstOrDefault() is null)
        {
            return;
        }
        EventModelEntity? eventFromDb = responce.FirstOrDefault();
        request.ResetUrl();
        InitialProperty(eventFromDb);
    }
    private async Task GetMembersTeam(int userId)
    {
        if(_user is null)
        {
            return;
        }
        var request = new ManagerGetRequests<UserModelEntity>();
        request.SetUrl($"GetAllTeamMembers?userId={userId}");
        var responce = await request.GetDataAsync(GetRequests.GetAllTeamMembers);
        if(responce != null)
        {
            foreach(var member in responce)
            {
                switch(member._goingToTheGame)
                {
                    case null:
                        Users.Add(member);
                        break;
                    case true:
                        UsersIsGoToTheGame.Add(member);
                        break;
                    case false:
                        UsersIsNotGoTheGame.Add(member);
                        break;
                }
            }
        }
    }
}