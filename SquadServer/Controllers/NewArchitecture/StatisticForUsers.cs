namespace SquadServer.Controllers.NewArchitecture;

[Route("api/statistic")]
public class StatisticForUsersController : ControllerBase
{
    private readonly SquadDbContext _context;
    private readonly StatisticForUserDbService _statisticForUserDbService;
    public StatisticForUsersController(SquadDbContext context)
    {
        _context = context;
        _statisticForUserDbService = new StatisticForUserDbService(_context);
    }

    [HttpGet("allInfo")]
    public async Task<IActionResult?> GetAllInfoUser(int userId)
    {
        try
        {
            UserAllInfoStatisticDTO? info = await _statisticForUserDbService.GetAllInfoUser(userId);
            return Ok(info);
        }
        catch(Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStatistickForUser(int commanderId, int userId)
    {
        try
        {
            UserAllInfoStatisticDTO? resultReader = await HttpContext.Request.ReadFromJsonAsync<UserAllInfoStatisticDTO>();
            if(resultReader is null)
                throw new NullReferenceException(nameof(resultReader));
            var result = await _statisticForUserDbService.UpdateStatisticForUser(commanderId, userId, resultReader);
            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}