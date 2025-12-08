using SquadApplication.Repositories.Enums;
using SquadApplication.Repositories.Interfaces;

namespace SquadApplication.Repositories.ManagerRequest;

public class ManagerPostRequests<T> : IRequestManager<T>
    where T : class
{
    public ManagerPostRequests()
    {
        _httpClient = new HttpClient();
    }
    private HttpClient _httpClient;
    public string _urlNameForSend { get; private set; } = "http://localhost:7176/";
    public int _currentStatusCode { get; private set; }



    public void SetUrl(string controllAction)
    {
        _urlNameForSend += controllAction;
    }
    public Task<List<T>> GetData(GetRequests getType)
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> PostRequests(PostsRequests postRequests)
    {
        throw new NotImplementedException();
    }
}
