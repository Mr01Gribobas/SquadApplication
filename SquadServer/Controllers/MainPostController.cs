namespace SquadServer.Controllers;

public class MainPostController:Controller
{

    private readonly SquadDbContext _squadDbContext;
    public MainPostController(SquadDbContext squadDb)
    {
        _squadDbContext = squadDb;
    }

    [HttpPost]
    public IActionResult? CreateEvent(int commanderId)
    {
        return null;
    }

    [HttpPost]
    public IActionResult? UpdateProfile(int userId)
    {
        return null;
    }

    [HttpPost]
    public IActionResult? CreateEquip(int userId)
    {
        return null;
    }
    [HttpPost]
    public IActionResult? UpdateEquip(int equipId)
    {
        return null;
    }
    [HttpPost]
    public IActionResult? AddReantils(int commanderId)
    {
        return null;
    }

    [HttpPost]
    public IActionResult? UpdateReantilsById(int reantilId, int userId)
    {
        return null;
    }

    [HttpPost]
    public IActionResult? AddPolygon(int userId)
    {
        return null;
    }
}
