using System.Net;

namespace SquadApplication.Repositories.ManagerRequest.UpgradeRequestManager;
public class BaseRequestsManager : IGetRequestManager,IPostRequestManager,IPutRequestManager,IPatchRequestManager,IDeleteRequestManager
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

    public async Task<T?> GetDateAsync<T>()
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl);
            T? resultFromServer = default(T?);
            if(response.StatusCode is HttpStatusCode.OK)
                resultFromServer = await response.Content.ReadFromJsonAsync<T>();
            return resultFromServer;
        }
        catch(Exception ex)
        {
            return default(T?);
        }
    }

    public async Task<bool> PostDateAsync<T>(T data) where T : class
    {
        var result = await _httpClient.PostAsJsonAsync(_baseUrl,data);
        bool readingResult = await result.Content.ReadFromJsonAsync<bool>();
        return readingResult;
    }

    public async Task<bool> PatchDateAsync<T>(T data) where T : class
    {
        throw new NotImplementedException();
    }

    public async Task<bool> PutDateAsync<T>(T data) where T : class
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteDateAsync()
    {
        throw new NotImplementedException();
    }
}

public interface IGetRequestManager
{
    Task<T> GetDateAsync<T>();
}

public interface IPostRequestManager
{
    Task<bool> PostDateAsync<T>(T data)where T:class ;
}

public interface IPutRequestManager
{
    Task<bool> PutDateAsync<T>(T data) where T : class;
}

public interface IPatchRequestManager
{
    Task<bool> PatchDateAsync<T>(T data) where T : class;
}

public interface IDeleteRequestManager
{
    Task<bool> DeleteDateAsync();
}
