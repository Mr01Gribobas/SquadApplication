namespace SquadApplication.ViewModels;

public partial class CreateEventsForAllCommandsViewModel : ObservableObject
{
    private readonly IUserSession _user;
    private readonly CreateEventsForAllCommandsPage _createEventPage;
    private readonly ManagerPostRequests<EventsForAllCommandsModelDTO> _postManager;
    //update
    //name game
    //
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




    public CreateEventsForAllCommandsViewModel(CreateEventsForAllCommandsPage createEventsPage, IUserSession user)
    {
        _user = user;
        _createEventPage = createEventsPage;
        _postManager = new ManagerPostRequests<EventsForAllCommandsModelDTO>();
        //teamNameOrganization = _user?.CurrentUser._nameUser
    }

    [RelayCommand]
    private async Task RequestFolCreateEvent()
    {
        if(_user.CurrentUser._role != Role.Commander)
            await _createEventPage.DisplayAlertAsync("Error", "You not commander", "Ok");

        try
        {
            ExaminationCoordinates();
            Validation();
            var commanderId =_createEventPage.CommanderId <= 0 ? _user.CurrentUser.Id : _createEventPage.CommanderId;
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
            await Shell.Current.GoToAsync("..");
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
                    throw new Exception("Coordinates is null");

            }
        }
    }
    private EventsForAllCommandsModelDTO CreateModel()
    {
        EventsForAllCommandsModelDTO newModel = new EventsForAllCommandsModelDTO(
            TeamNameOrganization: TeamNameOrganization,
            DescriptionShort: DescriptionShort,
            DescriptionFull: DescriptionFull,
            CoordinatesPolygon: CoordinatesPolygon,
            PolygonName: PolygonName ?? $"Not name polygon",
            Users: new List<UserModelEntity>() { _user.CurrentUser }
            );
        return newModel;

    }
}
