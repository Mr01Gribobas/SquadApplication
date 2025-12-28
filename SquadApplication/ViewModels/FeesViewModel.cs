using System.Text;
namespace SquadApplication.ViewModels;

public partial class FeesViewModel : ObservableObject
{
    public FeesViewModel(FeesPage feesPage, UserModelEntity user)
    {
        _feesPage = feesPage;
        _user = user;
        _requestManager = new ManagerGetRequests<EventModelEntity>();
        GetCurrentEvent();
        List<UserModelEntity> list = UserModelEntity.GetRandomData();
        Users = new ObservableCollection<UserModelEntity>(list);
    }
    private readonly FeesPage _feesPage;
    private readonly UserModelEntity _user;
    private readonly IRequestManager<EventModelEntity> _requestManager;


    [ObservableProperty]
    private ObservableCollection<UserModelEntity> users;

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



    private void InitialProperty(EventModelEntity? eventFromDb)
    {
        NamePolygon = eventFromDb.NamePolygon;
        CoordinatesPolygon = eventFromDb.Coordinates;
        DateAndTime = ConvertDateAndTime(eventFromDb.Date, eventFromDb.Time);
        CountMembers = eventFromDb.CountMembers.ToString();
    }

    private string? ConvertDateAndTime(DateOnly? date, TimeOnly? time)
    {
        
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append(date?.ToString()??"Not found");
        stringBuilder.Append(time?.ToString() ?? "Not found");
        return stringBuilder.ToString();
    }

    [RelayCommand]
    private async void CreateEvent()
    {
        await Shell.Current.GoToAsync($"/{nameof(CreateEventPage)}");
    }
    private async Task GetCurrentEvent()
    {
        if(_requestManager is null | _user is null)
            return;

        var request = (ManagerGetRequests<EventModelEntity>)_requestManager;
        request.SetUrl($"GetEvent?teamId={_user.TeamId}");
        List<EventModelEntity> responce = await request.GetDataAsync(GetRequests.GetEvent);

        if(responce is null ||
            responce.Count <= 0 ||
            responce.FirstOrDefault() is null)
        {
            return;
        }
        EventModelEntity eventFromDb = responce.FirstOrDefault();
        InitialProperty(eventFromDb);
    }
}