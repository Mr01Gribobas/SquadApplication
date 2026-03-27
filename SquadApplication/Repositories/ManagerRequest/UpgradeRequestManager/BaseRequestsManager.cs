using System.Net;

namespace SquadApplication.Repositories.ManagerRequest.UpgradeRequestManager;
public class BaseRequestsManager<T> : IGetRequestManager<T>
{
    private readonly HttpClient _httpClient;
    private  string _baseUrl = "http://10.0.2.2:5213/";
    public BaseRequestsManager(HttpClient httpClient) 
    {
        _httpClient = httpClient;
    }
    public void SetAddress(string address)
    {
        _baseUrl += address;
    }
    public void ResetAddress()
    {
        _baseUrl = "http://10.0.2.2:5213/";
    }

    public async Task<T> GetDateAsync()
    {
        HttpResponseMessage response =    await _httpClient.GetAsync(_baseUrl);
        if(response.StatusCode is HttpStatusCode.OK)
        {
            var res = await response.Content.ReadFromJsonAsync<T>()   ; 
        }
    }
}

public interface IGetRequestManager<T> 
{
    Task<T> GetDateAsync();
}
public interface IPostRequestManager<T>
{
    T PostDateAsync();
}
public interface IPutRequestManager<T>
{
    T GetDateAsync();
}
public interface IPatchRequestManager<T>
{
    T GetDateAsync();
}
public interface IDeleteRequestManager<T>
{
    T GetDateAsync();
}
