namespace SquadServer.Controllers.NewArchitecture;

[Route("api/polygons")]
public class PolygonsController : ControllerBase
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

    [HttpPost("createPolygon")]
    public async Task<IActionResult> AddPolygon(int userId)
    {
        PolygonEntity? resultReqding = await HttpContext.Request.ReadFromJsonAsync<PolygonEntity>();
        var resultoperation = await  _polygonDbService.AppendPolygon(resultReqding);
        return Ok(resultoperation);
    }

    [HttpDelete("deletePoligon")]
    public async Task<IActionResult?> DeletePolygonById(int poligonId)
    {
        Controller.LogInformation("Start action : GetAllReantil");
        bool result = await _polygonDbService.DeletePoligon(poligonId);
        return Ok(result);
    }

}
