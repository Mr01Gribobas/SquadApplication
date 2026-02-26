namespace SquadServer.Controllers;

public class MainPostController : Controller
{

    private readonly SquadDbContext _squadDbContext;
    private readonly EventNotificationDistributor _notificationDistributor;

    public MainPostController(SquadDbContext squadDb, EventNotificationDistributor notificationDistributor)
    {
        _squadDbContext = squadDb;
        _notificationDistributor = notificationDistributor;
    }

    [HttpPost]
    public async Task<IActionResult?> CreateEvent(int commanderId)
    {
        EventModelEntity? newEvent = await HttpContext.Request.ReadFromJsonAsync<EventModelEntity>();
        if(newEvent is null)
        {
            return Unauthorized();
        }

        if(_squadDbContext.Events.FirstOrDefault() is not null)
        {
            return Unauthorized();
        }

        var user = _squadDbContext.Players.Include(t => t.Team).FirstOrDefault(u => u.Id == commanderId);
        if(
            user is not null &&
            user.TeamId is not null &
            (user._role == Role.Commander |
            user._role == Role.AssistantCommander))
        {
            try
            {
                newEvent.Team = user.Team;
                newEvent.TeamId = (int)user.TeamId == 0 ? throw new NullReferenceException() : (int)user.TeamId;

                await _squadDbContext.Events.AddAsync(newEvent);
                await _squadDbContext.SaveChangesAsync();

                //var nottification = await _notificationDistributor.NotifyNewEventAsync(newEvent);
                return Ok();
            }
            catch(Exception ex)
            {
                return Unauthorized();
            }
        }
        return Unauthorized();
    }


    [HttpPost]
    public async Task<IActionResult?> CreateEventForAllCommands(int commanderId)
    {
        EventsForAllCommandsModelDTO? result = await HttpContext.Request.ReadFromJsonAsync<EventsForAllCommandsModelDTO>();

        try
        {
            EventsForAllCommandsModelEntity eventsModel = EventsForAllCommandsModelEntity.CreateModel(result, commanderId);
            await _squadDbContext.EventsForAllCommands.AddAsync(eventsModel);
            await _squadDbContext.SaveChangesAsync();
            return Ok(true);
        }
        catch(Exception ex)
        {

            return BadRequest(ex.Message);
        }
    }




    [HttpPost]
    public async Task<IActionResult?> UpdateProfile(int userId)
    {
        UserModelEntity userFromApp = await HttpContext.Request.ReadFromJsonAsync<UserModelEntity>();

        if(userFromApp == null)
        {
            return Unauthorized();
        }
        else
        {
            UserModelEntity? userEntity = _squadDbContext.Players.FirstOrDefault(eq => eq.Id == userId);
            if(userEntity == null)
            {
                return Unauthorized();
            }

            UserModelEntity.UpdateProfile(userFromApp, userEntity);
            _squadDbContext.SaveChanges();
            return Ok(userEntity);
        }
    }

    [HttpPost]
    public async Task<IActionResult?> CreateEquip(int userId , bool isCreate)//isCreate
    {
        EquipmentEntity? equipFromApp = await HttpContext.Request.ReadFromJsonAsync<EquipmentEntity>();
        try
        {
            if(equipFromApp == null | userId != equipFromApp?.OwnerEquipmentId)
                throw new Exception("Ошибка при десериализации");
            var userFromDb = await _squadDbContext.Players.FirstOrDefaultAsync(u => u.Id == userId);
            if(userFromDb is null)
                throw new Exception("Ошибка при попытке достать юреза");

            if(isCreate)
            {
                
            }

            equipFromApp.OwnerEquipment = userFromDb;
            await _squadDbContext.Equipments.AddAsync(equipFromApp);
            await _squadDbContext.SaveChangesAsync();
            userFromDb.Equipment = equipFromApp;


            //userFromDb.EquipmentId = equipFromApp.Id;
            userFromDb.UpdateStaffed(equipFromApp);

            await _squadDbContext.SaveChangesAsync();
            return Ok(equipFromApp);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }



    }
    [HttpPost]
    public async Task<IActionResult?> UpdateEquip(int equipId)
    {
        EquipmentEntity equipFromApp = await HttpContext.Request.ReadFromJsonAsync<EquipmentEntity>();
        if(equipFromApp == null)
        {
            return Unauthorized();
        }
        else
        {
            EquipmentEntity? equipEntity = await _squadDbContext.Equipments.Include(e => e.OwnerEquipment).FirstOrDefaultAsync(eq => eq.Id == equipId);
            if(equipEntity == null)
                return Unauthorized();

            EquipmentEntity.UpdateEquip(equipFromApp, equipEntity);
            equipEntity.OwnerEquipment.UpdateStaffed(equipEntity);

            await _squadDbContext.SaveChangesAsync();
            return Ok(equipEntity);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPolygon(int userId)
    {
        PolygonEntity? result = await HttpContext.Request.ReadFromJsonAsync<PolygonEntity>();
        if(result is null)
            return NotFound();
        try
        {
            if(!PolygonEntity.ValidateCoordinates(result.Coordinates))
                return BadRequest();

            await _squadDbContext.Polygons.AddAsync(result);
            await _squadDbContext.SaveChangesAsync();
            return Ok(result);
        }
        catch(Exception)
        {
            return BadRequest();
        }
    }



    [HttpPost]
    public async Task<IActionResult?> AddReantils(int commanderId)
    {
        RentailsDTO? result = await HttpContext.Request.ReadFromJsonAsync<RentailsDTO>();
        if(result is null || commanderId <= 0)
            return BadRequest();
        try
        {
            var commander = await _squadDbContext.Players.Include(t => t.Team).FirstOrDefaultAsync(u => u.Id == commanderId);

            await _squadDbContext.Reantils.AddAsync(new ReantalEntity()
            {
                TeamEntity = commander.Team,
                TeamId = commander.TeamId ?? throw new ArgumentNullException(),
                Weapon = result.Weapon,
                Mask = result.Mask,
                Helmet = result.Helmet,
                Balaclava = result.Balaclava,
                SVMP = result.SVMP,
                Outterwear = result.Outterwear,
                Gloves = result.Gloves,
                BulletproofVestOrUnloadingVest = result.BulletproofVestOrUnloadingVest,
                IsStaffed = result.Balaclava &&
                            result.Outterwear &&
                            result.BulletproofVestOrUnloadingVest &&
                            result.Gloves &&
                            result.SVMP &&
                            result.Helmet &&
                            result.Mask &&
                            result.Weapon
                            ? true : false,
            });
            await _squadDbContext.SaveChangesAsync();

            return Ok();
        }
        catch(Exception)
        {
            return BadRequest();

        }
    }

    [HttpPost]
    public async Task<IActionResult?> UpdateReantilsById(int reantilId)
    {
        RentailsDTO? result = await HttpContext.Request.ReadFromJsonAsync<RentailsDTO>();
        if(result is null || reantilId <= 0)
            return BadRequest();

        try
        {
            var rentaFromDb = await _squadDbContext.Reantils.FirstOrDefaultAsync(u => u.Id == reantilId);
            rentaFromDb.Weapon = result.Weapon;
            rentaFromDb.Mask = result.Mask;
            rentaFromDb.Helmet = result.Helmet;
            rentaFromDb.Balaclava = result.Balaclava;
            rentaFromDb.SVMP = result.SVMP;
            rentaFromDb.Outterwear = result.Outterwear;
            rentaFromDb.Gloves = result.Gloves;
            rentaFromDb.BulletproofVestOrUnloadingVest = result.BulletproofVestOrUnloadingVest;
            rentaFromDb.IsStaffed = result.Balaclava &&
                        result.Outterwear &&
                        result.BulletproofVestOrUnloadingVest &&
                        result.Gloves &&
                        result.SVMP &&
                        result.Helmet &&
                        result.Mask &&
                        result.Weapon
                        ? true : false;
            await _squadDbContext.SaveChangesAsync();
            return Ok();
        }
        catch(Exception)
        {
            return BadRequest();
        }
    }




}
