namespace SquadServer.Controllers.NewArchitecture;

[Route("api/polygons")]
public class PolygonsController:ControllerBase
{
    private readonly SquadDbContext _context;
    private readonly PolygonDbService _polygonDbService;

    public PolygonsController(SquadDbContext context)
    {
        _context = context;
        _polygonDbService = new PolygonDbService(_context);
    }
}
