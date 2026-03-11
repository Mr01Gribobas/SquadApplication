using SquadApplication.Repositories.ManagerRequest.Interfaces;

namespace SquadApplication.Repositories.ManagerRequest;

public class ManagerPostRequests<T> : IRequestManager<T>
    where T : class
{
    private HttpClient _httpClient;
    public string _urlNameForSend { get; private set; } = "http://10.0.2.2:5213/MainPost/";
    public int _currentStatusCode { get; private set; }

    public ManagerPostRequests()
    {
        _httpClient = new HttpClient();
        _httpClient.Timeout = TimeSpan.FromSeconds(60);
    }

    public void ResetUrlAndStatusCode()
    {
        _urlNameForSend = "http://10.0.2.2:5213/MainPost/";
        _currentStatusCode = 0;
    }
    public void SetUrl(string controllAction)=> _urlNameForSend += controllAction;

    public Task<List<T>> GetDataAsync(GetRequests getType)
    {
        throw new NotImplementedException();
    }

    private async Task<bool> PostRequest(T objectValue, string urlAction)
    {
        JsonContent jsonContent = JsonContent.Create(objectValue);
        var resultResponce = await _httpClient.PostAsJsonAsync<T>(_urlNameForSend, objectValue);
        if((int)resultResponce.StatusCode == 200 || (int)resultResponce.StatusCode == 201)
        {
            _currentStatusCode = (int)resultResponce.StatusCode;
            return true;
        }
        _currentStatusCode = (int)resultResponce.StatusCode;
        return false;
    }
    public async Task<bool> PostRequests(T objectValue, PostsRequests postRequests)
    {
        if(objectValue is null)
            throw new ArgumentNullException();

        switch(postRequests)
        {
            case PostsRequests.CreateEvent:
                return await PostRequest(objectValue, "CreateEvent");
            case PostsRequests.UpdateProfile:
                return await PostRequest(objectValue, "UpdateProfile");
            case PostsRequests.CreateEquip:
                return await PostRequest(objectValue, "CreateEquip");
            case PostsRequests.AddReantil:
                return await PostRequest(objectValue, "AddReantils");
            case PostsRequests.UpdateReantilsById:
                return await PostRequest(objectValue, "UpdateReantilsById");
            case PostsRequests.AddPolygon:
                return await PostRequest(objectValue, "AddPolygon");
            case PostsRequests.UpdateEquip:
                return await PostRequest(objectValue, "UpdateEquip");
            case PostsRequests.CreateEventForCommands:
                return await PostRequest(objectValue, "CreateEventForCommands");
            default:
                return false;
        }
    }
    public Task<bool> PutchRequestAsync(PutchRequest getType)
    {
        throw new NotImplementedException();
    }
}

