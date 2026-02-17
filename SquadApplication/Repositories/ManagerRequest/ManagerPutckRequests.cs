using SquadApplication.Repositories.ManagerRequest.Interfaces;

namespace SquadApplication.Repositories.ManagerRequest;

public class ManagerPutchRequests<T> : IRequestManager<T>
    where T : class
{



    public ManagerPutchRequests()
    {
        _httpClient = new HttpClient();
    }
    private HttpClient _httpClient;
    public string _urlNameForSend { get; private set; } = "http://10.0.2.2:5213/MainPutch/";
    public int _currentStatusCode { get; private set; }


    public void ResetUrlAndStatusCode()
    {
        _urlNameForSend = "http://10.0.2.2:5213/MainPutch/";
        _currentStatusCode = 0 ;
    }

    public void SetUrl(string controllAction)
    {
        _urlNameForSend += controllAction;
    }
    public async Task<bool> PutchRequestAsync(PutchRequest getType)
    {
        throw new NotImplementedException();
    }







    public Task<List<T>> GetDataAsync(GetRequests getType)
    {
        throw new NotImplementedException();
    }

    public Task<bool> PostRequests(T objectValue, PostsRequests postRequests)
    {
        throw new NotImplementedException();
    }


    
}
