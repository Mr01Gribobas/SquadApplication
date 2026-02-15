using Org.Apache.Http.Impl.Client;
using SquadServer.DTO_Classes.DTO_AuxiliaryModels;

namespace SquadApplication.ViewModels;

public partial class CreateEventsForAllCommandsViewModel:ObservableObject
{
    private readonly IUserSession _user;
    private readonly CreateEventsForAllCommandsPage _createEventPage;
    private readonly ManagerPostRequests<EventsForAllCommandsModelDTO> _postManager;
    

    public CreateEventsForAllCommandsViewModel(CreateEventsForAllCommandsPage createEventsPage , IUserSession user)
    {
        _user = user;
        _createEventPage = createEventsPage;
        _postManager = new ManagerPostRequests<EventsForAllCommandsModelDTO>();
    }
    private void CreateModel()
    {
        //EventsForAllCommandsModelDTO newModel = new EventsForAllCommandsModelDTO();
    }
}
