namespace SquadServer.Controllers.NewArchitecture;

[Route("api/equipments")]
public class EquipmentsController : ControllerBase
{
    private readonly SquadDbContext _context;
    private readonly EquipmentDbService _equipmentDbService;
    public EquipmentsController(SquadDbContext context)
    {
        _context = context;
        _equipmentDbService = new EquipmentDbService(_context);
    }


    [HttpGet("equipByUser")]
    public IActionResult? GetEquipByUserId(int userId)
    {
        Controller.LogInformation("Start action : GetEquipByUserId");

        var equip = _equipmentDbService.GetEquipByUserId(userId);
        List<EquipmentDTO> equipments = new List<EquipmentDTO>();
        if(equip is not null)
        {
            equipments.Add(new EquipmentDTO()
            {
                MainWeapon = equip.MainWeapon,
                NameMainWeapon = equip.NameMainWeapon,

                SecondaryWeapon = equip.SecondaryWeapon,
                NameSecondaryWeapon = equip.NameSecondaryWeapon,

                HeadEquipment = equip.HeadEquipment,
                BodyEquipment = equip.BodyEquipment,
                UnloudingEquipment = equip.UnloudingEquipment,
            });
        }
        return Ok(equipments);
    }

    [HttpPost("createById")]
    public async Task<IActionResult?> CreateEquip(int userId)
    {
        EquipmentDTO? equipFromApp = await HttpContext.Request.ReadFromJsonAsync<EquipmentDTO>();

        var result = await _equipmentDbService.CreateEquipment(userId, equipFromApp);
        return Ok(result);
    }

    [HttpPatch("updateById")]
    public async Task<IActionResult?> UpdateEquip(int equipId)
    {
        EquipmentDTO? equipFromApp = await HttpContext.Request.ReadFromJsonAsync<EquipmentDTO>();
        if(equipFromApp == null)
            return Unauthorized();
        var result = await _equipmentDbService.UpdateEquipment(equipId, equipFromApp);
        return Ok(result);
    }
}
