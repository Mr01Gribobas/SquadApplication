namespace SquadServer.Controllers.NewArchitecture;

[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly SquadDbContext _squadDbContext;
    private readonly UsersDbService _usersDbService;

    public UsersController(SquadDbContext squadDbContext)
    {
        _squadDbContext = squadDbContext;
        _usersDbService = new UsersDbService(_squadDbContext);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers(int userId)
    {
        if(userId <= 0)
            return BadRequest(400);
        List<UserModelEntity>? result = await _usersDbService.GetAllMembersAsync(userId);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult?> PlayerUpdateRank(int userId, bool rank)
    {
        var result = await _usersDbService.UpdateRankUser(userId, rank);

        return Ok(result);
    }

    [HttpGet("userId")]
    public IActionResult? GetUserById(int userId)
    {
        Controller.LogInformation("Start action : GetUserById");

        var user = _usersDbService.GetUserById(userId);
        if(user is null)
            return NotFound();
        return Ok(user);
    }    
}
