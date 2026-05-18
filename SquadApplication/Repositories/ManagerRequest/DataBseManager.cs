using SquadApplication.Models.Configurations;

namespace SquadApplication.Repositories;
public class DataBaseManager : IRequestManagerForEnter
{
    private readonly IUserSession _userSession;
    private readonly HttpClient _httpClient;
    private readonly IDeviceManager _deviceManager;
    public int _currentStatusCode { get; private set; }
    public int GetStatusCode() => _currentStatusCode;
    public DataBaseManager(IUserSession userSession, IDeviceManager deviceManager, HttpClient httpClient)
    {
        _httpClient =  httpClient;
        _userSession = userSession;
        _deviceManager = deviceManager;
    }
    public async Task<UserModelEntity> SendDataForRegistration(UserModelEntity user)
    {
        if(user is null)
            throw new ArgumentNullException();

        JsonContent content = JsonContent.Create(user);
        //ServerConfig.UseLocalNetwork();
        ServerConfig.UseEmulator();

        HttpResponseMessage responce = await _httpClient.PostAsync(ServerConfig._currentChanchedUrl + "/Imput/Registration", content);
        _currentStatusCode = (int)responce.StatusCode;
        if(_currentStatusCode == 200)
        {
            try
            {
                UserModelEntity? createdUser = await responce.Content.ReadFromJsonAsync<UserModelEntity>();
                _userSession.CurrentUser = createdUser;
                await _deviceManager.RegisterDeviceForCurrentUserAsync();//TODO WORK
                return createdUser;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        else if(_currentStatusCode == 201|| _currentStatusCode == 401)
            return null;
        return null;
    }

    public async Task<UserModelEntity> SendDataForEnter(string codeEnter)
    {
        int codePars = int.Parse((string)codeEnter);
        JsonContent content = JsonContent.Create(codePars);
        //ServerConfig.UseLocalNetwork();
        ServerConfig.UseEmulator();

        HttpResponseMessage responce = await _httpClient.GetAsync(ServerConfig._currentChanchedUrl + $"/Imput/Login?loginCode={codePars}");
        _currentStatusCode = (int)responce.StatusCode;

        if(_currentStatusCode == 200)
        {
            UserModelEntity? userFromServer = await responce.Content.ReadFromJsonAsync<UserModelEntity>();
            if(userFromServer is not null)
            {
                _userSession.CurrentUser = userFromServer;
                await _deviceManager.RegisterDeviceForCurrentUserAsync();//TODO WORK

            }
            return userFromServer;
        }
        return null;
    }
    public async Task LogoutAsync()
    {
        try
        {
            await _deviceManager.UnregisterDeviceForCurrentUserAsync();
            _userSession.CurrentUser = null;
            //delete user ok 
        }
        catch(Exception)
        {
            Console.WriteLine("Error work");
        }
    }
}
