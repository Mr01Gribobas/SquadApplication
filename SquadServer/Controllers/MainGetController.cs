using SquadServer.Repositoryes;

namespace SquadServer.Controllers;

public class MainGetController : Controller
{
    private readonly SquadDbContext _squadDbContext;
    private readonly DataBaseRepository _dataBaseRepository;

    public MainGetController(SquadDbContext squadDb)
    {
        _squadDbContext = squadDb;
        _dataBaseRepository = new DataBaseRepository(_squadDbContext);
    }
    [HttpGet]
    public IActionResult? GetAllTeamMembers(int teamId)
    {
        var listMembers = _dataBaseRepository.GetAllMembers(teamId);
        return Json(listMembers);
    }

    [HttpGet]
    public IActionResult? GetAllReantil(int teamId)
    {
        var list = _dataBaseRepository.GetAllReantil(teamId);
        return Json(list);
    }

    [HttpGet]
    public IActionResult? GetAllPolygons()
    {
        var list = _dataBaseRepository.GetAllPolygons();
        return Json(list);
    }

    [HttpGet]
    public IActionResult? GetAllEventsHistory()
    {
        var list = _dataBaseRepository.GetEventHistory();
        return Json(list);
    }

    [HttpGet]
    public IActionResult? GetEvent()
    {
          return Json(_dataBaseRepository.GeuEvent());
    }


    [HttpGet]
    public IActionResult? GetEquipById(int id)
    {
        var equip = _dataBaseRepository.GetEquipById(id);
        return Json(equip);
    }
}
