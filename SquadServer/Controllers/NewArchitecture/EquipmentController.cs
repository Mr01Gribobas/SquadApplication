namespace SquadServer.Controllers.NewArchitecture;

[Route("api/equipments")]
public class EquipmentsController:ControllerBase
{
    private readonly SquadDbContext _context;
    private readonly EquipmentDbService _equipmentDbService;
    public EquipmentsController(SquadDbContext context)
    {
        _context = context;
        _equipmentDbService = new EquipmentDbService(_context);
    }

}
