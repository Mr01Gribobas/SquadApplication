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
    public string GenerateDeviceToken()
    {
        throw new NotImplementedException();
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


