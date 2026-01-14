using SquadServer.Models;
using SquadServer.Repositoryes;
namespace SquadServer.Controllers;

[Controller]
public class ImputController : Controller
{
    private readonly SquadDbContext _squadDbContext;
    private readonly DataBaseRepository _dataBaseRepository;
    private readonly ILogger<ImputController> _logger;
    public ImputController(SquadDbContext squadDb, ILogger<ImputController> logger)
    {
        _squadDbContext = squadDb;
        _dataBaseRepository = new DataBaseRepository(squadDb);
        _logger = logger;
    }





    [HttpGet]
    public IActionResult Login(int loginCode)
    {
        if(loginCode < 0)
        {
            return Unauthorized();
        }
        UserModelEntity? Player = _dataBaseRepository?.
                                              GetUserFromDb(loginCode) ?? null;

        if(Player is null)
        {
            return Unauthorized();
        }
        return Ok(Player);
    }

    [HttpPost]
    public async Task<IActionResult>? Registration()
    {

        //HttpContext.Request.ContentType = "application/json";
        UserModelEntity? userFromApp = await HttpContext.Request.
                                      ReadFromJsonAsync<UserModelEntity>();

        if(!Validate(userFromApp))
        {
            return Unauthorized();//401
        }


        UserModelEntity? newUser = _dataBaseRepository.CreateNewUser(userFromApp);
        if(newUser is null)
        {
            return StatusCode(201);
        }

        return Ok(newUser);

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
            userFromApp._teamName is null |
            userFromApp._userName is null |
            userFromApp._callSing is null
            )
        {
            HttpContext.Response.StatusCode = 401;//nul propp
            return false;
        }

        if(userFromApp._phoneNumber[0] == '+')
        {
            string skipPlus = new String(userFromApp._phoneNumber?.Skip(1).ToArray());
            if(userFromApp._phoneNumber is null | !Int64.TryParse(skipPlus, out Int64 number))
            {

                return false;
            }
        }
        else if(userFromApp._phoneNumber is null | !Int64.TryParse(userFromApp._phoneNumber, out Int64 code))//error number)
        {
            HttpContext.Response.StatusCode = 402;//nul propp

            return false;
        }

        return true;
    }
}
