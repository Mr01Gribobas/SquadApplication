using SquadApplication.Repositories.Enums;
using SquadApplication.Repositories.Interfaces;
using System.Net.Http.Json;

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

    public Task<bool> PostRequests(T objectValue,PostsRequests postRequests)
    {
        switch(postRequests)
        {
            case PostsRequests.CreateEvent:
                //JsonContent jsonContent = JsonContent.Create();
                break;
            case PostsRequests.UpdateProfile:
                break;
            case PostsRequests.CreateEquip:
                break;
            case PostsRequests.AddReantils:
                break;
            case PostsRequests.UpdateReantilsById:
                break;
            case PostsRequests.AddPolygon:
                break;
            default:
                break;
        }
        return Task.FromResult(true);
    }
}
