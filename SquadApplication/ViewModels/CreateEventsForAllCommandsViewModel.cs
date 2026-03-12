namespace SquadApplication.ViewModels;

public partial class CreateEventsForAllCommandsViewModel : ObservableObject
{
    private readonly IUserSession _user;
    private readonly CreateEventsForAllCommandsPage _createEventPage;
    private readonly ManagerPostRequests<EventsForAllCommandsModelDTO> _postManager;
    private  bool _isUpdateThisGame;
    private  EventsForAllCommandsModelDTO _modelEventsForCommand;


    [ObservableProperty]
    private string nameGame;

    [ObservableProperty]
    private string teamNameOrganization;

    [ObservableProperty]
    private string descriptionShort;

    [ObservableProperty]
    private string descriptionFull;

    [ObservableProperty]
    private string coordinatesPolygon;

    [ObservableProperty]
    private string polygonName;

    [ObservableProperty]
    private string timeGame;

    [ObservableProperty]
    private string dategame;

    public CreateEventsForAllCommandsViewModel(CreateEventsForAllCommandsPage createEventsPage, IUserSession user)
    {
        _user = user;
        _createEventPage = createEventsPage;
        _postManager = new ManagerPostRequests<EventsForAllCommandsModelDTO>();
        TeamNameOrganization = _user?.CurrentUser._teamName ??  throw new NullReferenceException();
        if(_createEventPage._cache.GetItemByKey<EventsForAllCommandsModelDTO>("EventForCommands") is EventsForAllCommandsModelDTO model)
            InitialProperty(model);
        _createEventPage.DisplayAlertAsync("Info","При заполнении полного описания к игре -  Крайне рекомендуется описать данные для возможности связаться с вами(к примеру телефон ,ссылку ВК или ТГ) ","OK");
    }

    private void InitialProperty(EventsForAllCommandsModelDTO model)
    {
        if(model is null)
            return;

        NameGame = model.NameGame;
        TeamNameOrganization = model.TeamNameOrganization;
        DescriptionFull = model.DescriptionFull;
        DescriptionShort = model.DescriptionShort;
        CoordinatesPolygon = model.CoordinatesPolygon;
        PolygonName = model.PolygonName;
        TimeGame = model.Time.ToString();
        Dategame = model.Date.ToString();
        _modelEventsForCommand = model;
        _isUpdateThisGame = true; 
    }

    private async void BaseFullExamination()
    {
        try
        {
            if(_user.CurrentUser._role != Role.Commander)
                throw new Exception("You not commander");
            ExaminationCoordinates();
            Validation();
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [RelayCommand]
    private async Task RequestFolUpdateEvent()
    {
        try
        {
            BaseFullExamination();
            var commanderId = _createEventPage.CommanderId <= 0 ? _user.CurrentUser.Id : _createEventPage.CommanderId;
            EventsForAllCommandsModelDTO modelEvnt = CreateModel(_modelEventsForCommand);
            _postManager.SetUrl($"CreateEventForAllCommands?commanderId={commanderId}");
            var result = await _postManager.PostRequests(objectValue: modelEvnt, PostsRequests.CreateEventForCommands);
            if(result)
                await _createEventPage.DisplayAlertAsync("Ok", "Create is ok", "Ok");
            await Shell.Current.GoToAsync("..");
        }
        catch(Exception ex)
        {
            await _createEventPage.DisplayAlertAsync("Errir", $"{ex.Message}", "Ok");
        }
        finally
        {
            _postManager.ResetUrlAndStatusCode();
        }
    }

    [RelayCommand]
    private async Task RequestFolCreateEvent()
    {
        try
        {
            BaseFullExamination();
            var commanderId =_createEventPage.CommanderId <= 0 ? _user.CurrentUser.Id : _createEventPage.CommanderId;//
            EventsForAllCommandsModelDTO modelEvnt = CreateModel();
            _postManager.SetUrl($"CreateEventForAllCommands?commanderId={commanderId}");
            var result = await _postManager.PostRequests(objectValue: modelEvnt, PostsRequests.CreateEventForCommands);
            if(result)
                await _createEventPage.DisplayAlertAsync("Ok", "Create is ok", "Ok");
            await Shell.Current.GoToAsync("..");
        }
        catch(Exception ex)
        {
            await _createEventPage.DisplayAlertAsync("Errir", $"{ex.Message}", "Ok");            
        }
        finally
        {
            _postManager.ResetUrlAndStatusCode();
        }
    }
    private void Validation()
    {

        if(TeamNameOrganization != _user.CurrentUser._teamName)
            throw new Exception("Ошибка аутентификации перезеадите в приложение !!");
        if(string.IsNullOrEmpty(DescriptionShort))
            throw new Exception("Не коректное описание ");
        if(string.IsNullOrEmpty(DescriptionFull))
            throw new Exception("Не коректное описание ");
        if(string.IsNullOrEmpty(TimeGame) || !TimeOnly.TryParse(TimeGame , out var _))
            throw new Exception("Не верный формат времени. Используйте (60:24)");
        if(string.IsNullOrEmpty(Dategame)|| !DateOnly.TryParse(Dategame, out var _))
            throw new Exception("Не вурный формат даты. Используйте (30:12:2000)");
    }

    private void ExaminationCoordinates()
    {
        if(CoordinatesPolygon is null)
            throw new Exception("Coordinates is null");
        string coordinates = CoordinatesPolygon.Replace(" ", "");
        var coordinatesSplits = coordinates.Split(",");
        for(int i = 0; i < coordinatesSplits.Length; i++)
        {
            foreach(char item in coordinatesSplits[i])
            {
                if(item is '.' | item is '-')
                    continue;
                if(!int.TryParse(item.ToString(), out _))
                    throw new Exception("Неверный формат координат. Используйте (000000.00,0000,00) ");
            }
        }
    }
    private EventsForAllCommandsModelDTO CreateModel(EventsForAllCommandsModelDTO? modelDTO = null)
    {


        EventsForAllCommandsModelDTO newModel = new EventsForAllCommandsModelDTO(
            numberEvent: modelDTO is null ? 0 : modelDTO.numberEvent,
            NameGame: NameGame,
            TeamNameOrganization: TeamNameOrganization,
            DescriptionShort: DescriptionShort,
            DescriptionFull: DescriptionFull,
            CoordinatesPolygon: CoordinatesPolygon,
            PolygonName: PolygonName ?? $"Имя не было указано",
            UsersCount: 1,
            Date: DateOnly.Parse(Dategame),
            Time: TimeOnly.Parse(TimeGame)
            );
        return newModel;
    }
}