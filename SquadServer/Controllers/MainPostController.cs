using SquadServer.Models;
using System.Threading.Tasks;

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
        EquipmentEntity requipFromApp = await  HttpContext.Request.ReadFromJsonAsync<EquipmentEntity>();
        if(requipFromApp == null)
        {
            return Unauthorized();
        }
        else
        {
            _squadDbContext.Equipments.Add(requipFromApp);
            _squadDbContext.SaveChanges();
            return Ok(requipFromApp);
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
            EquipmentEntity? equipEntity = _squadDbContext.Equipments.FirstOrDefault(eq => eq.Id == equipId);
            if(equipEntity == null)
            {
                return Unauthorized();
            }

            EquipmentEntity.UpdateEquip(equipFromApp, equipEntity);
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
