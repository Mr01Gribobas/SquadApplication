namespace SquadApplication.ViewModels;

public partial class CreateEventViewModel : ObservableObject
{
    public CreateEventViewModel(CreateEventPage eventPage, UserModelEntity user)
    {
        _user = user;
        _eventPage = eventPage;
        _requestManager = new ManagerPostRequests<EventModelEntity>();
    }


    private IRequestManager<EventModelEntity> _requestManager;
    private readonly UserModelEntity _user;
    private readonly CreateEventPage _eventPage;

    [ObservableProperty]
    private string? date;    //Parse("20.12.2025:09:00:00:00").ToString();

    [ObservableProperty]
    private string? time;

    [ObservableProperty]
    private string? nameTeamEnemy;

    [ObservableProperty]
    private string? coordinatesPolygon;

    [ObservableProperty]
    private string? namePolygon;

    [RelayCommand]
    public async Task CreateEvent()
    {
        if(!Examination() ||
            _user is null ||
            _user._role != Role.Commander 
          )
        {
            return;//error
        }

        var newEvent = EventModelEntity.CreateEventModel(
            nameTeamEnemy: NameTeamEnemy,
            namePolygon: NamePolygon,
            coordinates: CoordinatesPolygon,
            time: TimeOnly.Parse(Time),
            date: DateOnly.Parse(Date),
            user: _user
            );

        var requestPost = (ManagerPostRequests<EventModelEntity>)_requestManager;
        requestPost.SetUrl($"CreateEvent?commanderId={_user.Id}");
        var resultCreated = await requestPost.PostRequests(newEvent, PostsRequests.CreateEvent);
        if(!resultCreated)
        {
            //error
        }
    }

    private bool Examination()
    {
        try
        {

            var dateOnly = DateOnly.Parse(Date); //"20.12.2025"
            var timeOnly = TimeOnly.Parse(Time);//10:10:10
            var coordinates = CoordinatesPolygon ?? throw new NullReferenceException();
            var namePolygon = NamePolygon ?? throw new NullReferenceException() ;
            var enemy = NameTeamEnemy ?? throw new NullReferenceException();
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }



}
