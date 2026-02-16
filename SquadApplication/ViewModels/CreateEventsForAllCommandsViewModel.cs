using SquadServer.DTO_Classes.DTO_AuxiliaryModels;

namespace SquadApplication.ViewModels;

public partial class CreateEventsForAllCommandsViewModel : ObservableObject
{
    private readonly IUserSession _user;
    private readonly CreateEventsForAllCommandsPage _createEventPage;
    private readonly ManagerPostRequests<EventsForAllCommandsModelDTO> _postManager;

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
    }


    private async Task RequestFolCreateEvent()
    {
        try
        {
            Validation();
            var modelEvnt = CreateModel();
            _postManager.SetUrl($"CreateEventForAllCommands?commanderId={_user.CurrentUser.Id}");
            var result = await _postManager.PostRequests(objectValue: modelEvnt, PostsRequests.CreateEventForCommands);
            if(result)
                await _createEventPage.DisplayAlertAsync("Ok", "Create is ok", "Ok");

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
        throw new NotImplementedException();
    }

    private EventsForAllCommandsModelDTO CreateModel()
    {
        EventsForAllCommandsModelDTO newModel = new EventsForAllCommandsModelDTO(
            TeamNameOrganization: TeamNameOrganization,
            DescriptionShort: DescriptionShort,
            DescriptionFull: DescriptionFull,
            CoordinatesPolygon: CoordinatesPolygon,
            PolygonName: PolygonName,
            Users: new List<UserModelEntity>() { _user.CurrentUser }
            );
        return newModel;

    }
}
