namespace SquadServer.Controllers.NewArchitecture;

[Route("api/rentales")]
public class RentalsController:ControllerBase
{
    private readonly SquadDbContext _context;
    private  readonly RentalDbService _rentalDbService;
    public RentalsController(SquadDbContext context)
    {
        _context = context;
        _rentalDbService = new RentalDbService(_context);
    }

    [HttpGet]
    public async Task<IActionResult?> GetAllReantil(int teamId)
    {
        Controller.LogInformation("Start action : GetAllReantil");
        if(teamId <= 0)
            return BadRequest();
        List<RentailsDTO> list = await _rentalDbService.GetAllReantilAsync(teamId);
        return Ok(list);
    }

    [HttpDelete("rentailNymber")]
    public async Task<IActionResult?> DeleteReantilById(int rentailNymber)
    {
        Controller.LogInformation("Start action : GetAllReantil");
        bool result = await _rentalDbService.DeleteRentail(rentailNymber);
        return Ok(result);
    }



}
