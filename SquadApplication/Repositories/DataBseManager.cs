
using SquadApplication.Repositories.Interfaces;
using System.Net.Http.Json;

namespace SquadApplication.Repositories;

public class DataBaseManager : IRequestManager
{
    public DataBaseManager()
    {
        _httpClient = new HttpClient();
    }
    private HttpClient _httpClient;
    private string _urlNameForSend = "https://localhost:7176/";

    public async Task<UserModelEntity> SendDataForRegistration(UserModelEntity user)
    {
        if(user is null)
            throw new ArgumentNullException();

        JsonContent content = JsonContent.Create(user);
       using HttpResponseMessage reasponce = await _httpClient.PostAsync(_urlNameForSend+ "Registration", content);

        return null;
    }

    public async Task<UserModelEntity> SendDataForEnter(string codeEnter)
    {
        int codePars = int.Parse((string)codeEnter);
        JsonContent content = JsonContent.Create(codePars);
        using HttpResponseMessage reasponce = await _httpClient.GetAsync(_urlNameForSend + $"Login?loginCode={codePars}");


        //_httpClient.post
        return null;

    }
}

