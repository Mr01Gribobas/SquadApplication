
using SquadApplication.Repositories.ManagerRequest.UpgradeRequestManager;

namespace SquadApplication.ViewModels;

public partial class CreateEventViewModel : ObservableObject
{

    private BaseRequestsManager _requestManager;
    private readonly UserModelEntity _user;
    private readonly CreateEventPage _eventPage;
    private bool _isUpdate;
    private EventModelEntity _modelFees;

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

    public CreateEventViewModel(CreateEventPage eventPage, UserModelEntity user)
    {
        _user = user;
        _eventPage = eventPage;
        _requestManager = new BaseRequestsManager(_eventPage._httpClientF.CreateClient());
        if(_eventPage._cache.GetItemByKey<EventModelEntity>("EventForUpdate") is EventModelEntity fees)
            InitialProperty(fees);
    }

    private void InitialProperty(EventModelEntity fees)
    {
        if(fees is null)
            return;
        _modelFees = fees;
        this.CoordinatesPolygon = fees.Coordinates;
        this.NameTeamEnemy = fees.NameTeamEnemy;
        this.NamePolygon = fees.NamePolygon;
        this.Date = fees.Date.ToString();
        this.Time = fees.Time.ToString();
        _isUpdate = true;
        _eventPage._cache.Remove("EventForUpdate");
    }

    [RelayCommand]
    public async Task CreateEvent()
    {
        if(!Examination() ||
            _user is null ||
            _user._role != Role.Commander
          )
            return;//error

        var newEvent = EventModelEntity.CreateEventModel(
            nameTeamEnemy: NameTeamEnemy,
            namePolygon: NamePolygon,
            coordinates: CoordinatesPolygon,
            time: TimeOnly.Parse(Time),
            date: DateOnly.Parse(Date),
            user: _user
            );
        if(_isUpdate && _modelFees is not null)
            newEvent.Id = _modelFees.Id;
        bool resultOperation;
        if(_isUpdate)
        {
            _requestManager.SetAddress($"api/events/updateFees?commanderId={_user.Id}");
            resultOperation = await _requestManager.PatchDateAsync<EventModelEntity>(newEvent);
        }
        else
        {
            _requestManager.SetAddress($"api/events/CreatedFees?commanderId={_user.Id}");
            resultOperation = await _requestManager.PostDateAsync<EventModelEntity>(newEvent);
        }
        if(!resultOperation)
        {
            await _eventPage.DisplayAlertAsync("Error", "Problems create event", "Ok");
            await Shell.Current.GoToAsync("..");
        }
        await _eventPage.DisplayAlertAsync("Ok", "Event create ok", "Ok");
        await Shell.Current.GoToAsync($"..?refresh={true}");
    }


    private bool Examination()
    {
        try
        {

            var dateOnly = DateOnly.Parse(Date); //"20.12.2025"
            var timeOnly = TimeOnly.Parse(Time);//10:10:10
            if(!ExaminationCoordinates())
                return false;
            var namePolygon = NamePolygon ?? throw new NullReferenceException();
            var enemy = NameTeamEnemy ?? throw new NullReferenceException();
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    private bool ExaminationCoordinates()
    {
        if(CoordinatesPolygon is null || CoordinatesPolygon.Length == 0 )
            return false;
        var slpitString = CoordinatesPolygon.Split(",");
        foreach(var stringCoordinates in slpitString)
            foreach(char _char in stringCoordinates)
                if(!int.TryParse(Convert.ToString(_char), out int number))
                    if(_char != '.')
                        return false;
        return true;
    }
}
