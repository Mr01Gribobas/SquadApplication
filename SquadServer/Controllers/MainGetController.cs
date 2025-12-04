namespace SquadServer.Controllers;

public class MainGetController : Controller
{
    private readonly SquadDbContext _squadDbContext;

    public MainGetController(SquadDbContext squadDb)
    {
        _squadDbContext = squadDb;
    }
    [HttpGet]
    public IActionResult? GetAllTeamMembers()
    {
        return null;
    }

    [HttpGet]
    public IActionResult? GetAllReantil()
    {
        return null;
    }

    [HttpGet]
    public IActionResult? GetAllPolygons()
    {
        return null;
    }

    [HttpGet]
    public IActionResult? GetAllEventById()
    {
        return null;
    }


    [HttpGet]
    public IActionResult? GetEquipById(int id)
    {
        return null;
    }
}
