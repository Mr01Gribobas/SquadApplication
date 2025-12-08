using SquadApplication.Repositories.Interfaces;
using System.ComponentModel.Design;
using System.Net.Http.Json;
namespace SquadApplication.Repositories;

public class DataBaseManager : IRequestManagerForEnter
{
    public DataBaseManager()
    {
        _httpClient = new HttpClient();
    }
    private HttpClient _httpClient;
    private string _urlNameForSend = "http://10.0.2.2:5213/Imput/";
    public int _currentStatusCode { get; private set; }
    public int GetStatusCode()=> _currentStatusCode;

    public async Task<UserModelEntity> SendDataForRegistration(UserModelEntity user)
    {
        if(user is null)
            throw new ArgumentNullException();

       

        JsonContent content = JsonContent.Create(user);

        using HttpResponseMessage responce = await _httpClient.PostAsync(_urlNameForSend+ "Registration", content);
        _currentStatusCode = (int)responce.StatusCode;
        if(_currentStatusCode == 200)
        {
            UserModelEntity? createdUser =  await responce.Content.ReadFromJsonAsync<UserModelEntity>();
            return createdUser;
        }
        else if(_currentStatusCode == 201)
        {
            //команды нету
        }
        else if(_currentStatusCode == 401)
        {
            //не прошел валидацию на сервере
        }

        return null; 
    }

    public async Task<UserModelEntity> SendDataForEnter(string codeEnter)
    {
        int codePars = int.Parse((string)codeEnter);
        JsonContent content = JsonContent.Create(codePars);
        using HttpResponseMessage responce = await _httpClient.GetAsync(_urlNameForSend + $"Login?loginCode={codePars}");

        _currentStatusCode = (int)responce.StatusCode;


        if(_currentStatusCode == 200)
        {
            UserModelEntity? userFromServer = await responce.Content.ReadFromJsonAsync<UserModelEntity>();
            return userFromServer;
        }
        else if(_currentStatusCode == 401)
        {
            //Либо код не верный либо пользователя с таким кодом нету
        }
        return null;
    }
}
