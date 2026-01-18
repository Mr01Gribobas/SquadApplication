using SquadApplication.Models.DeviceRegistrationModel;
using SquadApplication.Services.DeviceTokenService;
using System.Net.Http.Headers;

namespace SquadApplication.Repositories.NDeviceManager;

public interface IDeviceManager
{
    Task<bool> RegisterDeviceForCurrentUserAsync();
    Task<bool> UnregisterDeviceForCurrentUserAsync();
    Task<bool> UpdateDeviceTokenAsync();
    Task<bool> IsDeviceRegisteredAsync();
    Task<string> GetCurrentDeviceToken();
    Task<string> GetInstallationId();
}

