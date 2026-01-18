using SquadApplication.Models.DeviceRegistrationModel;
using SquadApplication.Services.DeviceTokenService;
using System.Net.Http.Headers;

namespace SquadApplication.Repositories.NDeviceManager;


public class DeviceManager : IDeviceManager
{
    private readonly HttpClient _httpClient;
    private readonly IUserSession _userSession;
    private readonly IConnectivity _connectivity;
    private readonly IDeviceTokenManager _deviceTokenService;
    private UserModelEntity _user => _userSession.CurrentUser;

    private string _urlRegistrationDevice = "http://10.0.2.2:5213/DeviceRegistartion/";
    private const string DeviceRegistrationKey = "device_registered";
    private const string DeviceTokenKey = "device_token";
    private const string InstallationIdKey = "installation_id";

    public DeviceManager(
               IConnectivity connectivity,
               HttpClient httpClient,
               IDeviceTokenManager tokenManager,
               IUserSession userSession
               )

    {
        _httpClient = httpClient;
        _userSession = userSession;
        _deviceTokenService = tokenManager;
        _connectivity = connectivity;


    }
    public bool SetUserFromSession(UserModelEntity user)
    {
        try
        {
            _userSession.CurrentUser = user;
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
    public async Task<string> GetCurrentDeviceToken()
    {
        var saveToken = await SecureStorage.GetAsync(DeviceTokenKey);

        if(!string.IsNullOrEmpty(saveToken))
        {
            return saveToken;
        }

        var newToken = await _deviceTokenService.GenerateDeviceToken();

        await SecureStorage.SetAsync(DeviceTokenKey, newToken);
        return newToken;
    }



    public async Task<string> GetInstallationId()
    {
        return await _deviceTokenService.GetOrCreateInstallationId();
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

            var installationId = await GetInstallationId();
            var token = await GetCurrentDeviceToken();

            var platform = await _deviceTokenService.GetPlatformAsync();

            var request = new DeviceRegistrationRequest()
            {
                DevicePlatform = platform,
                DeviceToken = token,
                InstallationId = installationId
            };//
            var content = JsonContent.Create(request);
            var responce = await _httpClient.PostAsync(_urlRegistrationDevice+$"RegistartionDevice?userId={_user.Id}", content);
            if(responce.IsSuccessStatusCode)
            {
                await SecureStorage.SetAsync(DeviceRegistrationKey, "true");
                Console.WriteLine("Registered ok");
                return true;

            }
            else
            {
                var error = await responce.Content.ReadAsStringAsync();
                Console.WriteLine("error");
                return false;
            }

        }
        catch(Exception)
        {
            return false;
        }

    }

    private async Task AddAuthorizationHeaderAsync()
    {
        var token = await GetAuthTokenAsync();

        if(!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }


    private async Task<string?> GetAuthTokenAsync()
    {
        return await SecureStorage.GetAsync("ayth_token");
    }

    public async Task<bool> IsDeviceRegisteredAsync()
    {
        var status = await SecureStorage.GetAsync(DeviceRegistrationKey);
        var isRegistoryLocal = status == "true";


        if(!isRegistoryLocal)
        {
            return false;
        }

        return true;
    }


    public async Task<bool> UpdateDeviceTokenAsync()
    {
        if(_userSession.CurrentUser is null)
        {
            return false;
        }

        try
        {
            await AddAuthorizationHeaderAsync();
            var installationId = await GetInstallationId();
            var newToken = await GetCurrentDeviceToken();

            var request = new DeviceTokenUpdateRequest()
            {
                InstallationId = installationId,
                NewToken = newToken,
            };

            var responce = await _httpClient.PutAsJsonAsync($"UpdateToken?userId={_user.Id}", request);
            return responce.IsSuccessStatusCode;
        }
        catch(Exception)
        {
            return false;
        }
    }
    public async Task<bool> UnregisterDeviceForCurrentUserAsync()
    {
        if(_userSession.CurrentUser == null)
        {
            return false;
        }
        if(_connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            return false;
        }

        try
        {
            await AddAuthorizationHeaderAsync();

            var installationId = await GetInstallationId();
            var responce = await _httpClient.PostAsync($"UnregisterDevice?{installationId}&userID={_user.Id}", null);

            if(responce.IsSuccessStatusCode)
            {
                SecureStorage.Remove(DeviceTokenKey);
                return true;
            }
            return false;
        }
        catch(Exception)
        {
            return false;
        }

    }
}