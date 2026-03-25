namespace SquadServer.Controllers.NewArchitecture;

[Route("api/statistic")]
public class StatisticForUsersController:ControllerBase
{
    private readonly SquadDbContext _context;
    private readonly StatisticForUserDbService _statisticForUserDbService;
    public StatisticForUsersController(SquadDbContext context)
    {
        _context = context;
        _statisticForUserDbService = new StatisticForUserDbService(_context);
    }

    [HttpGet("allInfo/{userId}")]
    public async Task<IActionResult?> GetAllInfoUser(int userId)
    {
        try
        {
            UserAllInfoStatisticDTO? info = await _statisticForUserDbService.GetAllInfoUser(userId);
            List<UserAllInfoStatisticDTO> listInfo = new List<UserAllInfoStatisticDTO>();
            listInfo.Add(info);
            return Ok(listInfo);
        }
        catch(Exception ex)
        {
            return BadRequest();
        }
    }
}
