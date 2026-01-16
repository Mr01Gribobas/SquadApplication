using System.Security.Cryptography;
using System.Text;

namespace SquadApplication.Services.DeviceTokenService;

public interface IDeviceTokenManager
{
    string GetOrCreateInstallationId();
    string GenerateDeviceToken();
    Task<string> GetPlatformAsync();
    string GetDeviceInfo();
}



