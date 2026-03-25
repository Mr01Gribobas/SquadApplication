namespace SquadServer.Controllers.NewArchitecture;

[Route("api/events")]
public class FeesAndEventsController
{
    private readonly SquadDbContext _context;
    private readonly FeesAndEventsDbService _EventsDbService;
    public FeesAndEventsController(SquadDbContext context)
    {
        _context = context;
        _EventsDbService = new FeesAndEventsDbService(_context);
    }

}
