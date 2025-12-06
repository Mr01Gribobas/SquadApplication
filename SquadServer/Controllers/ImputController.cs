using SquadServer.Models;
using SquadServer.Repositoryes;

namespace SquadServer.Controllers;


public class ImputController : Controller
{
    private readonly SquadDbContext _squadDbContext;
    private readonly DataBaseRepository _dataBaseRepository;

    public ImputController(SquadDbContext squadDb)
    {
        _squadDbContext = squadDb;
        _dataBaseRepository = new DataBaseRepository(squadDb);
    }

    [HttpGet]
    public IActionResult? Login(int loginCode)
    {
        if(loginCode < 0)
        {
            HttpContext.Response.StatusCode = 401;
            return null;
        }
        UserModelEntity? Player = _dataBaseRepository?.
                                              GetUserFromDb(loginCode) ?? null;

        if(Player is null)
        {
            HttpContext.Response.StatusCode = 401;
            return null;
        }

        HttpContext.Response.StatusCode = 201;
        return Json(Player);
    }

    [HttpPost]
    public async Task<IActionResult>? Registration()
    {
        UserModelEntity? userFromApp = await HttpContext.Request.
                                      ReadFromJsonAsync<UserModelEntity>();

        if(!Validate(userFromApp))
        {
            return null;
        }
        UserModelEntity? newUser = _dataBaseRepository.CreateNewUser(userFromApp);
        if(newUser is null)
        {
            HttpContext.Response.StatusCode = 201;
            return null;
        }

        HttpContext.Response.StatusCode = 200;
        return Json(newUser);
    }



    private bool Validate(UserModelEntity userFromApp)
    {
        if(userFromApp is null)
        {
            HttpContext.Response.StatusCode = 401;//nullUser
            return false;
        }

        if(
            userFromApp._phoneNumber is null |
            userFromApp.Team is null |
            userFromApp._userName is null |
            userFromApp._callSing is null
            )
        {
            HttpContext.Response.StatusCode = 401;//nul propp
            return false;
        }
        if(!int.TryParse(userFromApp._phoneNumber, out int code))//error number
        {
            HttpContext.Response.StatusCode = 402;//nul propp

            return false;
        }
        return true;
    }
}
