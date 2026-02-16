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


    [HttpPatch]
    public IActionResult? PlayerUpdateRank(int userId , bool rank)
    {
        return default(IActionResult?);
    }


    [HttpGet]
    public IActionResult? GetUserById(int Id)
    {
        Controller.LogInformation("Start action : GetUserById");

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
        Controller.LogInformation("Start action : GetAllTeamMembers");

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
        Controller.LogInformation("Start action : GetAllReantil");

        var list = _dataBaseRepository.GetAllReantil(teamId);
        return Json(list);
    }

    [HttpGet]
    public IActionResult? GetAllPolygons()
    {
        Controller.LogInformation("Start action : GetAllPolygons");

        var list = _dataBaseRepository.GetAllPolygons();
        return Json(list);
    }

    [HttpGet]
    public IActionResult? GetAllEventsHistory()
    {
        Controller.LogInformation("Start action : GetAllEventsHistory");

        var list = _dataBaseRepository.GetEventHistory();
        return Json(list);
    }

    [HttpGet]
    public IActionResult? GetEvent(int teamId)
    {
        Controller.LogInformation("Start action : GetEvent");

        var result = _dataBaseRepository.GetEvent(teamId);
        if(result == null)
        {
            return BadRequest();
        }


        List<EventModelEntity> events = new List<EventModelEntity>();
        events.Add(result);

        HttpContext.Response.StatusCode = 200;
        return Ok(events);
    }




    [HttpGet]
    public IActionResult? GetEquipByUserId(int userId)
    {
        Controller.LogInformation("Start action : GetEquipByUserId");

        var equip = _dataBaseRepository.GetEquipByUserId(userId);
        List<EquipmentEntity> equipments = new List<EquipmentEntity>();
        equipments.Add(equip);
        return Ok(equipments);
    }

    [HttpGet]
    public async Task<IActionResult?> GetAllInfoUser(int userId)
    {
        List<UserAllInfoStatisticDTO> listInfo = new List<UserAllInfoStatisticDTO>();
        UserAllInfoStatisticDTO? info = await _dataBaseRepository.GetAllInfoUser(userId);
        listInfo.Add(info);

        return Ok(listInfo);
    }

    [HttpGet]
    public async Task<IActionResult?> GetAllEventsForAllCommands()
    {
        List<EventsForAllCommandsModelDTO> events = await _dataBaseRepository.GetAllEventsForAllCommands();
        return Ok(events);

    }

    [HttpGet]
    public IActionResult? GetAllInfoForHomeProfile(int userId)
    {
        Controller.LogInformation("Start action : GetAllInfoForProfile");

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
    [HttpGet]
    public async Task<IActionResult> GameAttendance(int userId, bool isWill)
    {
        Controller.LogInformation("Start action : Login");

        try
        {
            var user = await _dataBaseRepository.GameAttendance(userId, isWill);
            var listUser = new List<UserModelEntity>();
            if(user is not null)
            {
                listUser.Add(user);
                return Ok(listUser);
            }
            return Unauthorized();

        }
        catch(Exception)
        {
            return Unauthorized();
        }
    }


}
