using SquadServer.Models;

namespace SquadServer.Controllers;

public class MainPostController : Controller
{

    private readonly SquadDbContext _squadDbContext;
    public MainPostController(SquadDbContext squadDb)
    {
        _squadDbContext = squadDb;
    }

    [HttpPost]
    public async Task<IActionResult?> CreateEvent(int commanderId)
    {
        EventModelEntity newEvent = await HttpContext.Request.ReadFromJsonAsync<EventModelEntity>();
        if(newEvent is null)
        {
            return Unauthorized();
        }
        var user = _squadDbContext.Players.FirstOrDefault(u => u.Id == commanderId);
        if(
            user is not null &&
            user.TeamId is not null &
            user._role == Role.Commander |
            user._role == Role.AssistantCommander)
        {
            newEvent.Team = user.Team;
            newEvent.TeamId = (int)user.TeamId;

            _squadDbContext.Events.Add(newEvent);
            _squadDbContext.SaveChanges();
            return Ok();
        }
        return Unauthorized();
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
    public async Task<IActionResult?> CreateEquip(int userId)
    {
        EquipmentEntity? equipFromApp = await HttpContext.Request.ReadFromJsonAsync<EquipmentEntity>();
        if(equipFromApp == null | userId != equipFromApp?.OwnerEquipmentId)
        {
            return Unauthorized();
        }
        else
        {
            _squadDbContext.Equipments.Add(equipFromApp);
            _squadDbContext.SaveChanges();
            var userFromDb = _squadDbContext.Players.FirstOrDefault(u => u.Id == userId);
            if(userFromDb is null)
            {
                return Unauthorized();
            }
            userFromDb.EquipmentId = equipFromApp.Id;
            userFromDb.Equipment = equipFromApp;
            if(!userFromDb.UpdateStaffed(equipFromApp))
            {
                return Unauthorized();
            }
            _squadDbContext.SaveChanges();
            return Ok(equipFromApp);
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
            EquipmentEntity? equipEntity = _squadDbContext.Equipments.Include(e => e.OwnerEquipment).FirstOrDefault(eq => eq.Id == equipId);
            if(equipEntity == null)
            {
                return Unauthorized();
            }

            EquipmentEntity.UpdateEquip(equipFromApp, equipEntity);
            if(!equipEntity.OwnerEquipment.UpdateStaffed(equipEntity))
            {
                return Unauthorized();
            }

            _squadDbContext.SaveChanges();
            return Ok(equipEntity);
        }
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
