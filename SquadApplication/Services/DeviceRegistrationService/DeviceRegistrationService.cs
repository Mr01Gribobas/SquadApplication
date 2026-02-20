namespace SquadApplication.Services.DeviceRegistrationService;


public class DeviceRegistrationService : IDeviceRegistrationService
{
    private readonly HttpClient _httpClient;
    private readonly IDeviceTokenManager _deviceTokenManager;
    private readonly IConnectivity _connectivity;
    private readonly IUserSession _userSession;
    private const string DeviceRegistrationKey = "device_registered";
    public bool IsDeviceRegistered { private set; get; }


    private UserModelEntity _user
    {
        get => _userSession.CurrentUser;
        set;
    }

    public DeviceRegistrationService(
                      IDeviceTokenManager deviceTokenManager,
                      IConnectivity connectivity,
                      HttpClient httpClient, IUserSession userSession)
    {
        _deviceTokenManager = deviceTokenManager;
        _connectivity = connectivity;
        _httpClient = httpClient;
        _userSession = userSession;

        CheckRegistrationStatus();
    }

    private async Task CheckRegistrationStatus()
    {
        var status = Task.Run(async () => await SecureStorage.GetAsync(DeviceRegistrationKey)).Result;
        IsDeviceRegistered = status == "true";
    }

    public async Task<bool> RegisterDeviceAsync()
    {
        if(_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            return false;
        }
        try
        {
            var installationId = _deviceTokenManager.GetOrCreateInstallationId();
            var deviceToken = _deviceTokenManager.GenerateDeviceToken();
            var platform = await _deviceTokenManager.GetPlatformAsync();

            var request = new
            {
                DeviceToken = deviceToken,
                Platform = platform,
                InstallationId = installationId,
            };

            var responce = await _httpClient.PostAsJsonAsync($"DeviceRegistartion/RegistartionDevice?userId={_user.Id}", request);

            if(responce.IsSuccessStatusCode)
            {
                await SecureStorage.SetAsync(DeviceRegistrationKey, "true");
                IsDeviceRegistered = true;

                return true;
            }
            else
            {
                return false;
            }
        }
        catch(Exception)
        {
            return false;
        }
    }

    public async Task<bool> UnregisterDeviceAsync()
    {
        if(_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            return false;
        }
        try
        {
            var installationId = _deviceTokenManager.GetOrCreateInstallationId();
            var responce = await _httpClient.PostAsync($"DeviceRegistartion/UnregisterDevice?installationId={installationId}&userID={_user.Id}", null);

            if(responce.IsSuccessStatusCode)
            {
                SecureStorage.Remove(DeviceRegistrationKey);
                IsDeviceRegistered = false;
                Console.WriteLine("Device unregistered");
                return true;
            }
            return false;

        }//TODO
        catch(Exception)
        {
            Console.WriteLine("Device not unregistered");
            return false;

        }
    }

    public async Task<bool> UpdateDeviceTokenAsync(string newToken)
    {
        if(_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            return false;
        }
        try
        {
            var instalationId = _deviceTokenManager.GetOrCreateInstallationId();
            var request = new { InstalationId = instalationId, NetToken = newToken };

            var responce = await _httpClient.PutAsJsonAsync("DeviceRegistartion/UpdateToken", request);
            return responce.IsSuccessStatusCode;
        }
        catch(Exception)
        {
            return false;
        }
    }
}
