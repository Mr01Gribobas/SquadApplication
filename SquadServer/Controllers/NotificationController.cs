using SquadServer.Repositoryes;

namespace SquadServer.Controllers;

[Controller]
public class NotificationController : Controller
{
    private readonly SquadDbContext _squadDbContext;
    private readonly DataBaseRepository _dataBaseRepository;
    public NotificationController(SquadDbContext squadDb)
    {
        _squadDbContext = squadDb;
        _dataBaseRepository = new DataBaseRepository(_squadDbContext);
    }



    [HttpGet]
    public async Task<IActionResult> CheckEventInDb(int teamId)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Start Check event");
        Console.ForegroundColor = ConsoleColor.White;

        if(await _dataBaseRepository.CheckEvent(teamId))
        {
            return Ok(true);
        }
        else
        {
            return Ok(false);
        }
    }
}
