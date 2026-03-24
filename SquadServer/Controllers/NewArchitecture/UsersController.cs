namespace SquadServer.Controllers.NewArchitecture;


[Route("api/users")]
public class UsersController:ControllerBase
{
    private readonly SquadDbContext _squadDbContext;

    public UsersController(SquadDbContext squadDbContext)
    {
        _squadDbContext = squadDbContext;
        _dataBaseRepository = new UserDbService(_squadDbContext);
    }

    [HttpGet("get")]
    public IActionResult GetUsers() 
    {
        return default(IActionResult);
    }
    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        Console.WriteLine(id);
        return default(IActionResult);
    }
}
