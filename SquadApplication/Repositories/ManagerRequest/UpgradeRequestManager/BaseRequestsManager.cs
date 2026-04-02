using System.Net;

namespace SquadApplication.Repositories.ManagerRequest.UpgradeRequestManager;

public class BaseRequestsManager : IRequestManager
{
    private readonly HttpClient _httpClient;
    private string _baseUrl = "http://10.0.2.2:5213/";
    public HttpStatusCode _statusCode;
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

    public async Task<T?> GetDateAsync<T>()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl);
            _statusCode = response.StatusCode;
            T? resultFromServer = default(T?);
            if(response.StatusCode is HttpStatusCode.OK)
            {
                resultFromServer = await response.Content.ReadFromJsonAsync<T>();
            }
            return resultFromServer;

        }
        catch(Exception ex)
        {
            return default(T?);
        }
    }

    public async Task<bool> PostDateAsync<T>(T data) where T : class
    {
        var result = await _httpClient.PostAsJsonAsync(_baseUrl, data);
        _statusCode = result.StatusCode;
        bool readingResult = await result.Content.ReadFromJsonAsync<bool>();
        return readingResult;
    }

    public async Task<bool> PatchDateAsync<T>(T data) where T : class
    {
        try
        {
            var response = await _httpClient.PatchAsJsonAsync(_baseUrl, data);
            bool result = await response.Content.ReadFromJsonAsync<bool>();
            return result;
        }
        catch(Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> PutDateAsync<T>(T data) where T : class
    {
        var response = await _httpClient.PutAsJsonAsync(_baseUrl, data);
        bool result = await response.Content.ReadFromJsonAsync<bool>();
        return result;
    }
    public async Task<bool> DeleteDateAsync()
    {
        bool response = await _httpClient.DeleteFromJsonAsync<bool>(_baseUrl);
        return response;
    }
}

public interface IRequestManager
{
    Task<T> GetDateAsync<T>();
    Task<bool> PostDateAsync<T>(T data) where T : class;
    Task<bool> PutDateAsync<T>(T data) where T : class;
    Task<bool> PatchDateAsync<T>(T data) where T : class;
    Task<bool> DeleteDateAsync();
}
