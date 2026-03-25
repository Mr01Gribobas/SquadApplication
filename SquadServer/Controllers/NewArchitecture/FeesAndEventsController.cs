namespace SquadServer.Controllers.NewArchitecture;

[Route("api/events")]
public class FeesAndEventsController : ControllerBase
{
    private readonly SquadDbContext _context;
    private readonly FeesAndEventsDbService _EventsDbService;
    public FeesAndEventsController(SquadDbContext context)
    {
        _context = context;
        _EventsDbService = new FeesAndEventsDbService(_context);
    }

    [HttpGet("EventHistory")]
    public IActionResult? GetAllEventsHistory()
    {
        Controller.LogInformation("Start action : GetAllEventsHistory");
        var list = _EventsDbService.GetEventHistory();
        return Ok(list);
    }

    [HttpGet("CurrentFees/{teamId}")]
    public async Task<IActionResult?> GetCurrentFees(int teamId)
    {
        Controller.LogInformation("Start action : GetEvent");
        var result = await _EventsDbService.GetEvent(teamId);

        if(result == null)
            return BadRequest();
        List<EventModelEntity> events = new List<EventModelEntity>();
        events.Add(result);
        HttpContext.Response.StatusCode = 200;
        return Ok(events);
    }

    [HttpGet("AllEventsForCommands")]
    public async Task<IActionResult?> GetAllEventsForAllCommands()
    {
        List<EventsForAllCommandsModelDTO> events = await _EventsDbService.GetAllEventsForAllCommands();
        return Ok(events);
    }

    [HttpGet("AppendOrDeleteFromTheMeeting/{nameteamOrganization}")]
    public async Task<IActionResult> AppendOrDeleteFromTheMeeting(string nameteamOrganization, int userId, bool turnout)
    {
        var res =  _EventsDbService.AppendOrDeleteFromTheMeeting(nameteamOrganization,userId,turnout);

    }

}
