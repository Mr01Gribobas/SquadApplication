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

    [HttpGet]
    public async Task<IActionResult?> GetAllPolygons()
    {
        Controller.LogInformation("Start action : GetAllPolygons");
        var list = await _polygonDbService.GetAllPolygons();
        return Ok(list);
    }

    [HttpDelete("deletePoligon")]
    public async Task<IActionResult?> DeletePolygonById(int poligonId)
    {
        Controller.LogInformation("Start action : GetAllReantil");
        bool result = await _polygonDbService.DeletePoligon(poligonId);
        return Ok(result);
    }

}
