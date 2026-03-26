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


    [HttpGet("AppendOrDeleteFromTheMeeting")]
    public async Task<IActionResult> AppendOrDeleteFromTheMeeting(string nameTeamOrganization, int userId, bool turnout)
    {
        if(nameTeamOrganization is null || userId <= 0)
            return BadRequest();
        var resultOperation = _EventsDbService.AppendOrDeleteFromTheMeeting(nameTeamOrganization, userId, turnout);
        return Ok(resultOperation);
    }
    [HttpPost]
    public async Task<IActionResult> CreateFees(int commanderId)
    {
        EventModelEntity? newEvent = await HttpContext.Request.ReadFromJsonAsync<EventModelEntity>();
        try
        {
            var resultOperation = _EventsDbService.CreateFees(commanderId, newEvent);
            return Ok(resultOperation);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPatch]//not full update
    public async Task<IActionResult?> UpdateEvent(int commanderId)
    {
        try
        {
            EventModelEntity? newEvent = await HttpContext.Request.ReadFromJsonAsync<EventModelEntity>();
            var result = await _EventsDbService.UpdateFees(commanderId, newEvent);
            return Ok(result);
        }
        catch(Exception ex)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult?> CreateEventForAllCommands(int commanderId)
    {
        EventsForAllCommandsModelDTO? modelResult = await HttpContext.Request.ReadFromJsonAsync<EventsForAllCommandsModelDTO>();
        try
        {
            var result = await _EventsDbService.CreateEventForAllCommands(commanderId, modelResult);
            return Ok(result);

        }
        catch(Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPatch]
    public async Task<IActionResult?> UpdateEventForAllCommands(int commanderId)
    {
        EventsForAllCommandsModelDTO? resultReading = await HttpContext.Request.ReadFromJsonAsync<EventsForAllCommandsModelDTO>();
        try
        {
            bool resultOperation = await _EventsDbService.UpdateEventsForAllCommands(commanderId, resultReading);
            return Ok(resultOperation);
        }
        catch(Exception ex)
        {
            return BadRequest();
        }
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteEventById(int commanderId, int numberEvent)
    {
        var result = await _EventsDbService.DeleteEventById(commanderId, numberEvent);
        return Ok(result);
    }
}

