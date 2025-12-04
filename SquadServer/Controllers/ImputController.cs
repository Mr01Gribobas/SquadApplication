namespace SquadServer.Controllers;


public class ImputController : Controller
{
    private readonly SquadDbContext _squadDbContext;

    public ImputController(SquadDbContext squadDb)
    {
        _squadDbContext = squadDb;
    }

    [HttpPost]
    public IActionResult? Login(int loginCode)
    {
        return null;
    }


    [HttpPost]
    public IActionResult? Registration()
    {
        return null;
    }

    
}
