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


    [HttpGet("equipByUser/{userId}")]
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
}
