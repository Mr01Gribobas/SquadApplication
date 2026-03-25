namespace SquadServer.Controllers.NewArchitecture;

[Route("api/users")]
public class UsersController:ControllerBase
{
    private readonly SquadDbContext _squadDbContext;
    private readonly UsersDbService _dbService;

    public UsersController(SquadDbContext squadDbContext)
    {
        _squadDbContext = squadDbContext;
        _dbService = new UsersDbService(_squadDbContext);
    }


    public async Task<IActionResult> GetUsers(int userId) 
    {
        if(userId <=0)
            return BadRequest(400);
        List<UserModelEntity>? result = await _dbService.GetAllMembersAsync(userId);
        return Ok(result);
    }
        

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetUserById(int id)
    //{
    //   var userFromDb = await _dbService.GetUserById(id);
    //}
}
