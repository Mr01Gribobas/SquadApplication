using Android.Print;
using SquadApplication.Services.DeviceRegistrationService;
using SquadApplication.Services.DeviceTokenService;
using System.Globalization;
using System.Net.Http.Headers;

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
    private readonly IDeviceTokenManager _deviceTokenService;

    private const string DeviceRegistrationKey = "device_registered";
    private const string DeviceTokenKey = "device_token";
    private const string InstallationIdKey = "installation_id";

    public DeviceManager(
               HttpClient client ,
               IUserSession userSession ,
               IConnectivity connectivity,
               IDeviceTokenManager tokenManager)
    {
        _connectivity = connectivity;
        _userSession = userSession;
        _deviceTokenService = tokenManager;
        _httpClient = client;

        OptionHttpClient();

    }

    private void OptionHttpClient()
    {
        _httpClient.BaseAddress = new Uri("http://10.0.2.2:5213/DeviceRegistartion");
        _httpClient.Timeout = TimeSpan.FromSeconds(60);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public string GetCurrentDeviceToken()
    {
        var saveToken = Task.Run(async () => await SecureStorage.GetAsync(DeviceTokenKey)).Result;

        if(!string.IsNullOrEmpty(saveToken))
        {
            return saveToken;
        }

        var newToken= _deviceTokenService.GenerateDeviceToken();

        Task.Run(async () => await SecureStorage.SetAsync(DeviceTokenKey,newToken));
        return newToken;
    }



    public string GetInstallationId()
    {
        return _deviceTokenService.GetOrCreateInstallationId();
    }


    public async Task<bool> RegisterDeviceForCurrentUserAsync()
    {
        if(_userSession.CurrentUser == null)
        {
            Console.WriteLine("User is null!!");
            return false;
        }
        if(_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            Console.WriteLine("Network is not acces");
            return false;
        }

        try
        {
            await AddAuthorizationHeaderAsync();

            var installationId = GetInstallationId();
            var token = GetCurrentDeviceToken();
            var platform = await _deviceTokenService.GetPlatformAsync();

            var request = new DeviceRegistrationRequest() 
            {
                
            };//

        }
        catch(Exception)
        {

            throw;
        }

    }

    private async Task AddAuthorizationHeaderAsync()
    {
        var token = await GetAuthTokenAsync();

        if(!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
        }
    }


    private async Task<string?> GetAuthTokenAsync()
    {
        return await SecureStorage.GetAsync("ayth_token");
    }

    public Task<bool> IsDeviceRegisteredAsync()
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
