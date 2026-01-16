using SquadApplication.Services.DeviceTokenService;

namespace SquadApplication.Services.DeviceRegistrationService;

public interface IDeviceRegistrationService
{
    Task<bool> RegisterDeviceAsync();
    Task<bool> UnregisterDeviceAsync();
    Task<bool> UpdateDeviceTokenAsync(string newToken);
    bool IsDeviceRegistered { get; }
}

