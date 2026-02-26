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
    public async Task<IActionResult?> PlayerUpdateRank(int userId , bool rank)
    {
        var result = await _dataBaseRepository.UpdateRankUser(userId,rank);

        return Ok(result);
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
    public async Task<IActionResult?> GetAllReantil(int teamId)
    {
        Controller.LogInformation("Start action : GetAllReantil");
        if(teamId <= 0)
            return BadRequest();

        List<RentailsDTO> list = await _dataBaseRepository.GetAllReantilAsync(teamId) ;
        if(list.Count ==0 || list is null)
            return BadRequest();
        return Ok(list);
    }


    [HttpDelete]
    public async Task<IActionResult?> DeleteReantilById(int rentailNymber)
    {
        Controller.LogInformation("Start action : GetAllReantil");

        bool result = await _dataBaseRepository.DeleteRentail(rentailNymber);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult?> GetAllPolygons()
    {
        Controller.LogInformation("Start action : GetAllPolygons");
        var list = await _dataBaseRepository.GetAllPolygons();
        return Ok(list);
    }


    [HttpGet]
    public async Task<IActionResult?> DeletePolygonsById(int poligonId)
    {
        Controller.LogInformation("Start action : GetAllReantil");

        bool result = await _dataBaseRepository.DeletePoligon(poligonId);
        return Ok(result);
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
        List<EquipmentDTO> equipments = new List<EquipmentDTO>();
        equipments.Add(new EquipmentDTO () 
        {
            MainWeapon = equip.MainWeapon,
            NameMainWeapon = equip.NameMainWeapon,

            SecondaryWeapon = equip.SecondaryWeapon,
            NameSecondaryWeapon = equip.NameSecondaryWeapon,

            HeadEquipment = equip.HeadEquipment,
            BodyEquipment = equip.BodyEquipment,
            UnloudingEquipment = equip.UnloudingEquipment,
        });
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
        Controller.LogInformation("Start action : GameAttendance");

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
