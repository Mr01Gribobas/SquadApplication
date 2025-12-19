using SquadServer.DTO_Classes;
using SquadServer.Models;
using SquadServer.Repositoryes;
using System.Text.Json;

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
    public IActionResult? GetUserById(int Id)
    {
        var user = _dataBaseRepository.GetUserById(Id);
        if(user is null)
        {
            HttpContext.Response.StatusCode = 401;
            return Json(null);

        }
        return Json(user);
    }


    [HttpGet]
    public IActionResult? GetAllTeamMembers(int userId)
    {
        try
        {
            var listMembers = _dataBaseRepository.GetAllMembers(userId);
            return Ok(listMembers);
        }
        catch(Exception ex)
        {

            return BadRequest(ex.Message);
        }
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
    public IActionResult? GetEvent(int teamId)
    {
        return Ok(_dataBaseRepository.GetEvent(teamId));
    }


    [HttpGet]
    public IActionResult? GetEquipByUserId(int userId)
    {
        var equip = _dataBaseRepository.GetEquipByUserId(userId);
        return Json(equip);
    }
    public IActionResult? GetAllInfoForProfile(int userId)
    {
        try
        {
            RequestTuplesManager requestTuples = new RequestTuplesManager(_dataBaseRepository);
            (UserModelEntity objectUser,
             TeamEntity objectTeam,
             EquipmentEntity? objectEquipment) infoForProfile = requestTuples.GetInfoForProfileById(userId);

            var container = new TripleContainerDTO<UserModelEntity, TeamEntity, EquipmentEntity>()
            {
                _itemOne = infoForProfile.objectUser,
                _itemTwo = infoForProfile.objectTeam,
                _itemThree = infoForProfile.objectEquipment
            };
            
            var jsonData3 = JsonSerializer.Serialize(container);
            return Ok(jsonData3);

        }
        catch(Exception ex)
        {
            return Unauthorized();
        }
    }
}
