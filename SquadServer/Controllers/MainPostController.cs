namespace SquadServer.Controllers;

public class MainPostController:Controller
{

    private readonly SquadDbContext _squadDbContext;
    public MainPostController(SquadDbContext squadDb)
    {
        _squadDbContext = squadDb;
    }

    [HttpPost]
    public IActionResult? CreateEvent()
    {
        return null;
    }

    [HttpPost]
    public IActionResult? UpdateProfile(int userId)
    {
        return null;
    }

    [HttpPost]
    public IActionResult? CreateEquip()
    {
        return null;
    }

    [HttpPost]
    public IActionResult? AddReantils()
    {
        return null;
    }

    [HttpPost]
    public IActionResult? UpdateReantilsById(int reantilId)
    {
        return null;
    }

    [HttpPost]
    public IActionResult? AddPolygon()
    {
        return null;
    }
}
