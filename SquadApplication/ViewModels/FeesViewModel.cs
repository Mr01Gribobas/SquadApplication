using SquadApplication.Repositories.ManagerRequest.Interfaces;

namespace SquadApplication.ViewModels;

public partial class FeesViewModel : ObservableObject
{

    private readonly FeesPage _feesPage;
    private readonly IUserSession _user;
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

    public FeesViewModel(FeesPage feesPage, IUserSession user)
    {
        _feesPage = feesPage;
        _user = user;
        _requestManager = new ManagerGetRequests<EventModelEntity>();
        GetCurrentEvent();

    }
    

    [RelayCommand]
    public async void CurrentHumanWillBe()
    {
        var isWill = true;
        ManagerGetRequests<UserModelEntity> request = CreateGetRequestUserModel(isWill);
        var responce = await request.GetDataAsync(GetRequests.GameAttendance);
        if(request._currentStatusCode == 200 | request._currentStatusCode == 204)
        {
            UpdateLists(responce.FirstOrDefault());
        }
        request.ResetUrlAndStatusCode();

    }

    [RelayCommand]
    public async void CurrentHumanWillNot()
    {
        var isWill = false;
        ManagerGetRequests<UserModelEntity> request = CreateGetRequestUserModel(isWill);
        List<UserModelEntity?> responce = await request.GetDataAsync(GetRequests.GameAttendance) as List<UserModelEntity>;//getUser
        if(request._currentStatusCode == 200 | request._currentStatusCode == 204)
        {
            UpdateLists(responce.FirstOrDefault());
        }

        request.ResetUrlAndStatusCode();
    }




    private ManagerGetRequests<UserModelEntity> CreateGetRequestUserModel(bool isWill)
    {
        var request = new ManagerGetRequests<UserModelEntity>();
        request.SetUrl($"GameAttendance?userId={_user.CurrentUser.Id}&isWill={isWill}");
        return request;
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
        {
            return;
        }
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
        request.SetUrl($"GetEvent?teamId={_user.CurrentUser.TeamId}");
        List<EventModelEntity>? responce = await request.GetDataAsync(GetRequests.GetEvent);

        if(responce is null ||
            responce.Count <= 0 ||
            responce.FirstOrDefault() is null)
        {
            return;
        }
        EventModelEntity? eventFromDb = responce.FirstOrDefault();
        request.ResetUrlAndStatusCode();
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
                SortUsers(member);
            }
        }
    }


}