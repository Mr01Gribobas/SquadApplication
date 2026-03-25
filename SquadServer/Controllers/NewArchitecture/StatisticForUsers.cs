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
}
