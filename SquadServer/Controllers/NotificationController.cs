using SquadServer.Extension;
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
    public async Task<IActionResult> CheckEventInDb(int teamId, int userId)
    {
        Controller.LogInformation("Start action : CheckEventInDb");
        if(userId <=0 || teamId <= 0)
        {
            return Ok(new EvenCheck(isGoTogame: false, availabilityEvent: false));
        }
       EvenCheck event_chek =  await _dataBaseRepository.CheckEvent(teamId, userId);
            return Ok(event_chek);
    }
    public record class EvenCheck(bool availabilityEvent, bool isGoTogame);
}
