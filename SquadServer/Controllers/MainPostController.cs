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
            return Unauthorized();
        var user = _squadDbContext.Players.Include(t => t.Team).FirstOrDefault(u => u.Id == commanderId);
        try
        {
            if(
                user is not null &&
                user.TeamId is not null &
               (user._role == Role.Commander |
                user._role == Role.AssistantCommander))
            {
                if(_squadDbContext.Events.FirstOrDefault(e => e.TeamId == user.TeamId) is not null)
                    return Unauthorized("Event is ok");
                newEvent.Team = user.Team;
                newEvent.TeamId = (int)user.TeamId == 0 ? throw new NullReferenceException() : (int)user.TeamId;
                await _squadDbContext.Events.AddAsync(newEvent);
                await _squadDbContext.SaveChangesAsync();
                //var nottification = await _notificationDistributor.NotifyNewEventAsync(newEvent);
                return Ok();
            }
            else
                throw new NullReferenceException();
        }
        catch(Exception ex)
        {
            return Unauthorized();
        }
    }

    [HttpPost]
    public async Task<IActionResult?> UpdateEvent(int commanderId)
    {
        EventModelEntity? newEvent = await HttpContext.Request.ReadFromJsonAsync<EventModelEntity>();
        if(newEvent is null)
            return Unauthorized();

        var user = _squadDbContext.Players.Include(t => t.Team).FirstOrDefault(u => u.Id == commanderId);

        try
        {
            if(user is not null && user.TeamId is not null)
            {
                var oldEvent = await _squadDbContext.Events.Include(t => t.Team).FirstOrDefaultAsync(t => t.TeamId == user.TeamId);
                if(oldEvent is not null)
                {
                    oldEvent.NameTeamEnemy = newEvent.NameTeamEnemy;
                    oldEvent.NamePolygon = newEvent.NamePolygon;
                    oldEvent.Coordinates = newEvent.Coordinates;
                    oldEvent.Time = newEvent.Time;
                    oldEvent.Date = newEvent.Date;
                    _squadDbContext.Events.Update(oldEvent);
                }
                else
                {
                    await _squadDbContext.Events.AddAsync(new EventModelEntity
                    {
                        NameTeamEnemy = newEvent.NameTeamEnemy,
                        Coordinates = newEvent.Coordinates,
                        NamePolygon = newEvent.NamePolygon,
                        Date = newEvent.Date,
                        Time = newEvent.Time,

                        TeamId = (int)user.TeamId,
                        Team = user?.Team ?? throw new InvalidOperationException()
                    });

                }
                await _squadDbContext.SaveChangesAsync();
                return Ok();
            }
            else
                throw new NullReferenceException();
        }
        catch(Exception ex)
        {
            return Unauthorized();
        }
    }
    

    [HttpPost]
    public async Task<IActionResult?> CreateEventForAllCommands(int commanderId)
    {
        EventsForAllCommandsModelDTO? result = await HttpContext.Request.ReadFromJsonAsync<EventsForAllCommandsModelDTO>();

        try
        {
            var commander = await _squadDbContext.Players.Include(t => t.Team).FirstOrDefaultAsync(u => u.Id == commanderId);
            if(commander is not null && result is not null)
            {
                EventsForAllCommandsModelEntity eventsModel = EventsForAllCommandsModelEntity.CreateModel(result, commander);

                await _squadDbContext.EventsForAllCommands.AddAsync(eventsModel);
                await _squadDbContext.SaveChangesAsync();
                return Ok(true);
            }
            else
                throw new Exception();

        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult?> UpdateEventForAllCommands(int commanderId)
    {
        EventsForAllCommandsModelDTO? result = await HttpContext.Request.ReadFromJsonAsync<EventsForAllCommandsModelDTO>();

        try
        {
            var commander = await _squadDbContext.Players.Include(t => t.Team).FirstOrDefaultAsync(u => u.Id == commanderId);
            if(commander is not null && result is not null && commander.Team is not null)
            {

                var eventFromDb = await _squadDbContext.EventsForAllCommands.FirstOrDefaultAsync(e => e.Id == result.numberEvent);
                if(eventFromDb is not null)
                {
                    eventFromDb.NameGame = result.NameGame;
                    eventFromDb.DescriptionFull = result.DescriptionFull;
                    eventFromDb.DescriptionShort = result.DescriptionShort;
                    eventFromDb.PolygonName = result.PolygonName;
                    eventFromDb.CoordinatesPolygon = result.CoordinatesPolygon;
                    eventFromDb.DateAndTimeGame = new DateTime(result.Date, result.Time);
                    _squadDbContext.EventsForAllCommands.Update(eventFromDb);
                }
                else
                {
                    EventsForAllCommandsModelEntity eventsModel = EventsForAllCommandsModelEntity.CreateModel(result, commander);
                    await _squadDbContext.EventsForAllCommands.AddAsync(eventsModel);
                }
                await _squadDbContext.SaveChangesAsync();

                return Ok(true);
            }
            else
                throw new Exception();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpPost]
    public async Task<IActionResult?> UpdateProfile(int userId)
    {
        UserModelEntity? userFromApp = await HttpContext.Request.ReadFromJsonAsync<UserModelEntity>();

        if(userFromApp == null)
            return StatusCode(400);
        else
        {
            UserModelEntity? userEntity = await _squadDbContext.Players.FirstOrDefaultAsync(eq => eq.Id == userId);
            if(userEntity is not null)
            {
                UserModelEntity.UpdateProfile(userFromApp, userEntity);
                _squadDbContext.Players.Update(userEntity);
            }
            else
                return StatusCode(400);

            await _squadDbContext.SaveChangesAsync();
            return Ok(userEntity);
        }
    }

    [HttpPost]
    public async Task<IActionResult?> CreateEquip(int userId)
    {
        EquipmentDTO? equipFromApp = await HttpContext.Request.ReadFromJsonAsync<EquipmentDTO>();

        try
        {
            if(equipFromApp == null)
                throw new Exception("Ошибка при десериализации");

            var userFromDb = await _squadDbContext.Players.Include(eq => eq.Equipment).FirstOrDefaultAsync(u => u.Id == userId);
            if(userFromDb is not null)
            {

                if(userFromDb.Equipment is not null)
                {
                    userFromDb.Equipment.MainWeapon = equipFromApp.MainWeapon;
                    userFromDb.Equipment.NameMainWeapon = equipFromApp.NameMainWeapon;

                    userFromDb.Equipment.SecondaryWeapon = equipFromApp.SecondaryWeapon;
                    userFromDb.Equipment.NameSecondaryWeapon = equipFromApp.NameSecondaryWeapon;

                    userFromDb.Equipment.HeadEquipment = equipFromApp.HeadEquipment;
                    userFromDb.Equipment.BodyEquipment = equipFromApp.BodyEquipment;
                    userFromDb.Equipment.UnloudingEquipment = equipFromApp.UnloudingEquipment;
                    _squadDbContext.Equipments.Update(userFromDb.Equipment);
                }
                else
                {
                    var newEquip = EquipmentEntity.CreateModelEntity(equipFromApp);
                    newEquip.OwnerEquipment = userFromDb;
                    newEquip.OwnerEquipmentId = userFromDb.Id;
                    userFromDb.Equipment = newEquip;
                    await _squadDbContext.Equipments.AddAsync(newEquip);

                }
            }
            else
                throw new Exception("Ошибка при попытке достать юреза");

            userFromDb?.UpdateStaffed(userFromDb?.Equipment ?? throw new NullReferenceException());
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
        EquipmentDTO? equipFromApp = await HttpContext.Request.ReadFromJsonAsync<EquipmentDTO>();
        if(equipFromApp == null)
            return Unauthorized();

        EquipmentEntity? equipEntity = await _squadDbContext.Equipments.Include(e => e.OwnerEquipment).FirstOrDefaultAsync(eq => eq.Id == equipId);

        try
        {

            if(equipEntity is not null)
            {
                equipEntity.MainWeapon = equipFromApp.MainWeapon;
                equipEntity.NameMainWeapon = equipFromApp.NameMainWeapon;

                equipEntity.SecondaryWeapon = equipFromApp.SecondaryWeapon;
                equipEntity.NameSecondaryWeapon = equipFromApp.NameSecondaryWeapon;

                equipEntity.UnloudingEquipment = equipFromApp.UnloudingEquipment;
                equipEntity.HeadEquipment = equipFromApp.HeadEquipment;
                equipEntity.BodyEquipment = equipFromApp.BodyEquipment;
                _squadDbContext.Equipments.Update(equipEntity);

                if(equipEntity.OwnerEquipment is not null)
                    equipEntity.OwnerEquipment.UpdateStaffed(equipEntity);

                await _squadDbContext.SaveChangesAsync();
            }
            else
            {
                UserModelEntity? userFromDb = await _squadDbContext.Players.
                                                                   Include(eq => eq.Equipment).
                                                                   FirstOrDefaultAsync(u => u.EquipmentId == equipId);
                if(userFromDb is not null)
                {
                    if(userFromDb.Equipment is not null)
                    {
                        userFromDb.Equipment.MainWeapon = equipFromApp.MainWeapon;
                        userFromDb.Equipment.NameMainWeapon = equipFromApp.NameMainWeapon;

                        userFromDb.Equipment.SecondaryWeapon = equipFromApp.SecondaryWeapon;
                        userFromDb.Equipment.NameSecondaryWeapon = equipFromApp.NameSecondaryWeapon;

                        userFromDb.Equipment.UnloudingEquipment = equipFromApp.UnloudingEquipment;
                        userFromDb.Equipment.HeadEquipment = equipFromApp.HeadEquipment;
                        userFromDb.Equipment.BodyEquipment = equipFromApp.BodyEquipment;
                        _squadDbContext.Equipments.Update(userFromDb.Equipment);
                        userFromDb.UpdateStaffed(userFromDb.Equipment);

                    }
                    else
                    {

                        EquipmentEntity newEquip = EquipmentEntity.CreateModelEntity(equipFromApp);
                        newEquip.OwnerEquipment = userFromDb;
                        newEquip.OwnerEquipmentId = userFromDb.Id;
                        userFromDb.Equipment = newEquip;
                        await _squadDbContext.Equipments.AddAsync(newEquip);
                        userFromDb.UpdateStaffed(newEquip);
                    }
                    await _squadDbContext.SaveChangesAsync();
                }
                else
                    throw new NullReferenceException();

            }


            return Ok(equipEntity);//status code ??
        }
        catch(Exception ex)
        {
            return Ok(null);
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

        try
        {
            if(result is null || commanderId <= 0)
                throw new NullReferenceException();

            var commander = await _squadDbContext.Players.Include(t => t.Team).FirstOrDefaultAsync(u => u.Id == commanderId);
            ReantalEntity model = null;

            if(commander is not null && commander.Team is not null)
            {
                model = new ReantalEntity()
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
                };
                await _squadDbContext.Reantils.AddAsync(model);
                await _squadDbContext.SaveChangesAsync();
            }

            if(model is null)
                throw new NullReferenceException();

            return Ok(model);

        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);

        }
    }

    [HttpPost]
    public async Task<IActionResult?> UpdateReantilsById(int reantilId)
    {
        RentailsDTO? result = await HttpContext.Request.ReadFromJsonAsync<RentailsDTO>();


        try
        {
            if(result is null || reantilId <= 0)
                throw new NullReferenceException();

            var rentaFromDb = await _squadDbContext.Reantils.FirstOrDefaultAsync(u => u.Id == reantilId);
            if(rentaFromDb is not null)
            {
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
                _squadDbContext.Reantils.Update(rentaFromDb);
            }

            await _squadDbContext.SaveChangesAsync();
            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEventById(int commanderId, int numberEvent)
    {
        EventsForAllCommandsModelEntity? result = await _squadDbContext.EventsForAllCommands.Include(p=>p.Players).FirstOrDefaultAsync(ev => ev.Id == numberEvent);
        if(result is not null)
        {
            result.Players.Clear();
            _squadDbContext.EventsForAllCommands.Remove(result);
            await _squadDbContext.SaveChangesAsync();
            return Ok(true);
        }
        else
            return Ok(false);

    }


}
