using System.Security.Cryptography;
using System.Text;

namespace SquadApplication.Services.DeviceTokenService;

public interface IDeviceTokenManager
{
    Task<string> GetOrCreateInstallationId();
    Task<string> GenerateDeviceToken();
    Task<string> GetPlatformAsync();
    string GetDeviceInfo();
}



