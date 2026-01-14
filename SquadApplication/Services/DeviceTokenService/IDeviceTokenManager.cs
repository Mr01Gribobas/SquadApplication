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
public class DeviceTokenManager : IDeviceTokenManager
{
    private const string InstallationIdKey = "installation_id";
    private const string DeviceTokenKey = "device_token";
    public string GenerateDeviceToken()
    {
        var existingToken = Task.Run(async () => await SecureStorage.GetAsync(DeviceTokenKey)).Result;
        if(!string.IsNullOrEmpty(existingToken))
        {
            return existingToken;
        }

        var newToken = GenerateDeviceTokenInternal();
    }//TODO

    private object GenerateDeviceTokenInternal()
    {
        var installationId = GetOrCreateInstallationId().ToString();
        var platform = Task.Run(async () => await GetPlatformAsync()).Result;
        var timestamp = DateTime.UtcNow.Ticks;
        var baseToken = $"{platform}_{installationId}_{timestamp}";

        using var sha256 = SHA256.Create();

        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(baseToken));
        var token = $"LOCAL_{Convert.ToBase64String(hash).
                                   Replace("/", "_").
                                   Replace("+", "-").
                                   Replace("=", "").
                                   Substring(0, 32)}";


        Console.WriteLine($"Token is created : {token}");
        return token;
    }

    public string GetDeviceInfo()
    {
        throw new NotImplementedException();
    }

    public string GetOrCreateInstallationId()
    {
        throw new NotImplementedException();
    }

    public Task<string> GetPlatformAsync()
    {
        throw new NotImplementedException();
    }
}


