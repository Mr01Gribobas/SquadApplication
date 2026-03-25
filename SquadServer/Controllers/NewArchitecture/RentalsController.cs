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
}
