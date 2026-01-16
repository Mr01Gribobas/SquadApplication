using SquadApplication.Services.DeviceTokenService;

namespace SquadApplication.Services.DeviceRegistrationService;

public interface IDeviceRegistrationService
{
    Task<bool> RegisterDeviceAsync();
    Task<bool> UnregisterDeviceAsync();
    Task<bool> UpdateDeviceTokenAsync(string newToken);
    bool IsDeviceRegistered { get; }
}
public class DeviceRegistrationService:IDeviceRegistrationService
{
    private readonly HttpClient _httpClient;
    private readonly IDeviceTokenManager _deviceTokenManager;
    private readonly IConnectivity _connectivity;

    private const string DeviceRegistrationKey = "device_registered";

    public bool IsDeviceRegistered {  private set; get; }

    public DeviceRegistrationService(
                      IDeviceTokenManager deviceTokenManager ,
                      IConnectivity connectivity ,
                      HttpClient httpClient)
    {
        _deviceTokenManager = deviceTokenManager;
        _connectivity = connectivity;
        _httpClient = httpClient;


        CheckRegistrationStatus();
    }

    private async Task CheckRegistrationStatus()
    {
        var status = Task.Run( async () => await SecureStorage.GetAsync(DeviceRegistrationKey)).Result;
        IsDeviceRegistered = status == "true" ; 
    }

    public Task<bool> RegisterDeviceAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UnregisterDeviceAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateDeviceTokenAsync(string newToken)
    {

    }
}
