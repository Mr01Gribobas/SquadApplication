
using SquadApplication.Repositories.ManagerRequest.UpgradeRequestManager;

namespace SquadApplication.ViewModels;

public partial class FeesViewModel : ObservableObject
{

    private readonly FeesPage _feesPage;
    private readonly IUserSession _user;
    private readonly BaseRequestsManager _requestManager;
    private EventModelEntity _event;


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

    public FeesViewModel(FeesPage feesPage, IUserSession user)
    {
        _feesPage = feesPage;
        _user = user;
        _requestManager = new BaseRequestsManager(_feesPage._clientFactory.CreateClient());
        GetCurrentEvent();
    }


    [RelayCommand]
    public async void CurrentHumanWillBe()
    {
        var isWill = true;
        await RequestAsync(isWill);
        _requestManager.ResetAddress();
    }


    private async Task RequestAsync(bool isWill)
    {
        _requestManager.SetAddress($"api/users/GameAttendance?userId={_user.CurrentUser.Id}&isWill={isWill}");
        bool result = await _requestManager.PatchDateAsync<UserModelEntity>(null);
        if(result)
        {
            _user.CurrentUser._goingToTheGame = true;
            UpdateLists(_user.CurrentUser);
        }
    }

    [RelayCommand]
    public async void CurrentHumanWillNot()
    {
        var isWill = false;
        await RequestAsync(isWill);
        _requestManager.ResetAddress();
    }

    private void UpdateLists(UserModelEntity user)
    {
        if(user is null)
            return;
        SortUsers(user);
    }
    private void SortUsers(UserModelEntity member)
    {
        switch(member._goingToTheGame)
        {
            case null:
                if(Users.FirstOrDefault(u => u.Id == member.Id) is null)
                {
                    Users.Add(member);
                    if(UsersIsGoToTheGame.FirstOrDefault(u => u.Id == member.Id) is not null)
                        UsersIsGoToTheGame.Remove(UsersIsGoToTheGame.FirstOrDefault(u => u.Id == member.Id));
                    if(UsersIsNotGoTheGame.FirstOrDefault(u => u.Id == member.Id) is not null)
                        UsersIsNotGoTheGame.Remove(UsersIsNotGoTheGame.FirstOrDefault(u => u.Id == member.Id));
                }
                break;
            case true:
                if(UsersIsGoToTheGame.FirstOrDefault(u => u.Id == member.Id) is null)
                {
                    UsersIsGoToTheGame.Add(member);
                    if(Users.FirstOrDefault(u => u.Id == member.Id) is not null)
                        Users.Remove(Users.FirstOrDefault(u => u.Id == member.Id));
                    if(UsersIsNotGoTheGame.FirstOrDefault(u => u.Id == member.Id) is not null)
                        UsersIsNotGoTheGame.Remove(UsersIsNotGoTheGame.FirstOrDefault(u => u.Id == member.Id));
                }
                break;
            case false:
                if(UsersIsNotGoTheGame.FirstOrDefault(u => u.Id == member.Id) is null)
                {
                    UsersIsNotGoTheGame.Add(member);
                    if(UsersIsGoToTheGame.FirstOrDefault(u => u.Id == member.Id) is not null)
                        UsersIsGoToTheGame.Remove(UsersIsGoToTheGame.FirstOrDefault(u => u.Id == member.Id));
                    if(Users.FirstOrDefault(u => u.Id == member.Id) is not null)
                        Users.Remove(Users.FirstOrDefault(u => u.Id == member.Id));
                }
                break;
        }
    }

    private async void InitialProperty(EventModelEntity? eventFromDb)
    {
        if(eventFromDb is null)
            return;

        _event = eventFromDb;
        NameTeamEnemu = eventFromDb.NameTeamEnemy;
        NamePolygon = eventFromDb.NamePolygon;
        CoordinatesPolygon = eventFromDb.Coordinates;
        DateAndTime = ConvertDateAndTime(eventFromDb.Date, eventFromDb.Time);
        CountMembers = eventFromDb.CountMembers.ToString();
        await GetMembersTeam(_user.CurrentUser.Id);
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
        if(_feesPage is null)
            await Shell.Current.GoToAsync($"/{nameof(CreateEventPage)}");
        else
        {
            _feesPage._cacheService.Set<EventModelEntity>("EventForUpdate", _event);
            await Shell.Current.GoToAsync($"/{nameof(CreateEventPage)}");
        }
    }





    [RelayCommand]
    private async void CopyCoordinates()
    {
        if(CoordinatesPolygon is null)
            return;
        await Clipboard.Default.SetTextAsync(CoordinatesPolygon);
    }




    public async Task GetCurrentEvent()
    {
        if(_requestManager is null | _user is null)
            return;

        //var request = (ManagerGetRequests<EventModelEntity>)_requestManager;
        _requestManager.SetAddress($"api/events/CurrentFees/{_user.CurrentUser.TeamId}");
        EventModelEntity? responce = await _requestManager.GetDateAsync<EventModelEntity>();

        if(responce is null)
            return;
        _requestManager.ResetAddress();
        InitialProperty(responce);
    }

    private async Task GetMembersTeam(int userId)
    {
        if(_user is null)
            return;
        _requestManager.SetAddress($"api/users/allUsers?userId={userId}");
        List<UserModelEntity>? responce = await _requestManager.GetDateAsync<List<UserModelEntity>>();
        if(responce != null && responce.Count != 0 )
        {
            foreach(var member in responce)
                SortUsers(member);
        }
    }
}
