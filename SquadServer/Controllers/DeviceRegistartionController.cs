using SquadServer.DTO_Classes;
using SquadServer.Services.Service_DeviceRegistration;

namespace SquadServer.Controllers;

[Controller]
public class DeviceRegistartionController:ControllerBase
{
    private readonly IDeviceRegistrationService _deviceRegistrationService;
    public DeviceRegistartionController(IDeviceRegistrationService registrationService)
    {
        _deviceRegistrationService = registrationService;
    }

    [HttpPost]
    public async Task<IActionResult> RegistartionDevice()
    {
       var dtoRegistration =  await HttpContext.Request.ReadFromJsonAsync<DeviceRegistrationDTO>();
        return default;//TODO
    }
}
