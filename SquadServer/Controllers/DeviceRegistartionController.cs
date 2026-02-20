namespace SquadServer.Controllers;

[Controller]
public class DeviceRegistartionController : Controller
{
    private readonly IDeviceRegistrationService _deviceRegistrationService;
    public DeviceRegistartionController(IDeviceRegistrationService registrationService)
    {
        _deviceRegistrationService = registrationService;
    }

    
    [HttpPost]
    public async Task<IActionResult> RegistartionDevice(int userId)
    {
        Controller.LogInformation("Start action : RegistartionDevice");

        var dtoRegistration = await HttpContext.Request.ReadFromJsonAsync<DeviceRegistrationDTO>();
        if(dtoRegistration is null)
            return Unauthorized();

        try
        {
            if(userId > 0 | userId != 0)
            {
                await _deviceRegistrationService.RegisterDeviceAsync(
                    dtoDevice: dtoRegistration,
                    userId: userId
                    );
                return Ok(new { Success = true, Message = "Register ok" });
            }
            return StatusCode(500);

        }
        catch(Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> UnregisterDevice(string installationId, int userID)
    {
        Controller.LogInformation("Start action : UnregisterDevice");

        if(string.IsNullOrEmpty(installationId) | userID <= 0)
        {
            return StatusCode(401);
        }
        try
        {
            await _deviceRegistrationService.UnregisterDeviceAsync(installationId, userID);
            return Ok(new { Success = true, Message = "Operation ok" });
        }
        catch(Exception ex)
        {
            return Unauthorized();
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateToken(int userId)
    {
        Controller.LogInformation("Start action : UpdateToken");

        var dtoUpdateToken = await HttpContext.Request.ReadFromJsonAsync<DeviceTokenUpdateDTO>();
        try
        {
            await _deviceRegistrationService.UpdateDeviceTokenAsync(dtoUpdateToken.InstallationId, dtoUpdateToken.TokenUpdate, userId);
            return Ok(new { Success = true, Message = "Update ok" });
        }
        catch(Exception)
        {
            return Unauthorized();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetTokens(int userId)
    {
        Controller.LogInformation("Start action : GetTokens");

        try
        {
            var token = await _deviceRegistrationService.GetUserDeviceTokensAsync(userId);
            return Ok(new { Token = token });
        }
        catch(Exception)
        {
            return Unauthorized();
        }
    }


}
