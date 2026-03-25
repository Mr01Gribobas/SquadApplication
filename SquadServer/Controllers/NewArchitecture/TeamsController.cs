namespace SquadServer.Controllers.NewArchitecture;

[Route("api/teams")]
public class TeamsController:ControllerBase
{
    private readonly SquadDbContext _contextDb;
    private readonly TeamDbService _teamDbService;
    public TeamsController(SquadDbContext context)
    {
        _contextDb = context;
        _teamDbService = new TeamDbService(context);
    }
}
