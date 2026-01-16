using System.Security.Cryptography;
using System.Text;

namespace SquadApplication.Services.DeviceTokenService;


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


        if(newToken is not null)
        {
            Task.Run(async () => await SecureStorage.SetAsync(DeviceTokenKey, newToken)).Wait();
            return newToken;
        }
        return null;
    }//TODO

    private string GenerateDeviceTokenInternal()
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
    public string GetOrCreateInstallationId()
    {
        var exitstingId = Task.Run(async () => await SecureStorage.GetAsync(InstallationIdKey)).Result;

        if(!string.IsNullOrEmpty(exitstingId))
        {
            return exitstingId;
        }

        var newId = GenerateInstallationId();

        Task.Run(async () => await SecureStorage.SetAsync(InstallationIdKey, newId)).Wait();

        return newId;

    }
    private string GenerateInstallationId()
    {//TODO
        var deviceId = GetDeviceUniqueId();
        var timestamp = DateTime.UtcNow.Ticks;
        var random = new Random().Next(1000, 9999);

        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes($"{deviceId}_{timestamp}_{random}"));

        return Convert.ToBase64String(hash).Replace("/", "_").Replace("+", "-").Replace("=", "").Substring(0, 22);
    }

    private string GetDeviceUniqueId()
    {
        try
        {
#if ANDROID
            var context = Android.
                         App.
                         Application.
                         Context;

            var deviceId = Android.
                         Provider.
                         Settings.
                         Secure.
                         GetString(context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            if(!string.IsNullOrEmpty(deviceId) & deviceId != "9774d56d682e549c")
            {
                return $"android_{deviceId}";
            }//TODO
#endif

#if IOS
            Console.WriteLine("Test #IF");

            var deviceId =  UIKit.UIDevice.CurrentDevice.IdentifierForVendor?.ToString();

                if (!string.IsNullOrEmpty(deviceId))
                    return $"ios_{deviceId}";
#endif

            return $"{DeviceInfo.Model}_{DeviceInfo.Platform}_{DeviceInfo.VersionString}";
        }
        catch(Exception)
        {
            return Guid.NewGuid().ToString();
        }
    }

    public string GetDeviceInfo()
    {
        return $"{DeviceInfo.Model} ({DeviceInfo.Platform} {DeviceInfo.Version})";
    }


    public async Task<string> GetPlatformAsync()
    {
        return DeviceInfo.Platform.ToString().ToLower();
    }
}