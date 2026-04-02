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

    [HttpGet("allUsers")]
    public async Task<IActionResult> GetAllUsers(int userId)
    {
        if(userId <= 0)
            return BadRequest(400);
        List<UserModelEntity>? result = await _usersDbService.GetAllMembersAsync(userId);
        return Ok(result);
    }

    [HttpPut("UpdateRank")]
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

    [HttpGet("GetAllInfoForHome")]
    public async Task<IActionResult?> GetAllInfoForHomeProfile(int userId)
    {
        Controller.LogInformation("Start action : GetAllInfoForProfile");

        try
        {
            if(userId <= 0)
                throw new Exception();
            var resultJson = await _usersDbService.GetAllInfoForHomeProfile(userId);
            if(resultJson is null)
                return BadRequest();
            return Ok(resultJson);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("GameAttendance")]
    public async Task<IActionResult> GameAttendance(int userId, bool isWill)
    {
        Controller.LogInformation("Start action : GameAttendance");
        try
        {
            var result = await _usersDbService.GameAttendance(userId, isWill);
            return Ok(true);
        }
        catch(Exception ex)
        {
            return Ok(false);
        }
    }

    [HttpPatch("UpdateShorPtofile")]
    public async Task<IActionResult?> UpdateProfile(int userId)
    {
        UserModelEntity? userFromApp = await HttpContext.Request.ReadFromJsonAsync<UserModelEntity>();
        var result = _usersDbService.UpdateProfileById(userId, userFromApp);
        return Ok(result);
    }
}
