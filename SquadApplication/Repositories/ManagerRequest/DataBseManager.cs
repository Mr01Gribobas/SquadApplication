using SquadApplication.Repositories.NDeviceManager;

namespace SquadApplication.Repositories;

public class DataBaseManager : IRequestManagerForEnter
{
    public DataBaseManager(IUserSession userSession, IDeviceManager deviceManager )
    {
        _httpClient = new HttpClient();
        _userSession = userSession;
        _deviceManager = deviceManager;
    }
    private readonly IUserSession _userSession;
    private readonly HttpClient _httpClient;
    private readonly IDeviceManager _deviceManager;


    private string _urlNameForSend = "http://10.0.2.2:5213/Imput/";
    public int _currentStatusCode { get; private set; }
    public int GetStatusCode() => _currentStatusCode;


    public async Task<UserModelEntity> SendDataForRegistration(UserModelEntity user)
    {
        if(user is null)
            throw new ArgumentNullException();

        JsonContent content = JsonContent.Create(user);

        HttpResponseMessage responce = await _httpClient.PostAsync(_urlNameForSend + "Registration", content);
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

                throw;
            }
        }
        else if(_currentStatusCode == 201)
        {
            return null;
        }
        else if(_currentStatusCode == 401)
        {
            return null;

        }
        return null;
    }

    public async Task<UserModelEntity> SendDataForEnter(string codeEnter)
    {
        int codePars = int.Parse((string)codeEnter);
        JsonContent content = JsonContent.Create(codePars);

        HttpResponseMessage responce = await _httpClient.GetAsync(_urlNameForSend + $"Login?loginCode={codePars}");
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
        else if(_currentStatusCode == 401)
        {
            //Либо код не верный либо пользователя с таким кодом нету
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
