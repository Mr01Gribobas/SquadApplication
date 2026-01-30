using SquadServer.Repositoryes;

namespace SquadServer.Controllers;

[Controller]
public class NotificationController : Controller
{
    private readonly SquadDbContext _squadDbContext;
    private readonly DataBaseRepository _dataBaseRepository;
    private int sum;
    public NotificationController(SquadDbContext squadDb)
    {
        _squadDbContext = squadDb;
        _dataBaseRepository = new DataBaseRepository(_squadDbContext);
    }



    [HttpGet]
    public async Task<IActionResult> CheckEventInDb(int teamId)
    {
        sum += 1;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Check number : {sum}");
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
