using Android.Print;

namespace SquadApplication.Repositories.DeviceManager;

public interface IDeviceManager
{
    Task<bool> RegisterDeviceForCurrentUserAsync();
    Task<bool> UnregisterDeviceForCurrentUserAsync();
    Task<bool> UpdateDeviceTokenAsync();
    Task<bool> IsDeviceRegisteredAsync();
    string GetCurrentDeviceToken();
    string GetInstallationId();
}
public class DeviceManager : IDeviceManager
{
    private readonly HttpClient _httpClient;
    private readonly IUserSession _userSession;
    private readonly IConnectivity _connectivity;



    public string GetCurrentDeviceToken()
    {
        throw new NotImplementedException();
    }

    public string GetInstallationId()
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsDeviceRegisteredAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> RegisterDeviceForCurrentUserAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UnregisterDeviceForCurrentUserAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateDeviceTokenAsync()
    {
        throw new NotImplementedException();
    }
}
