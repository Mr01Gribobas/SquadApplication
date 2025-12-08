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

    private async Task<bool> PostRequest(T objectValue, string urlAction)
    {
        JsonContent jsonContent = JsonContent.Create(objectValue);
        var resultResponce = await _httpClient.PostAsJsonAsync<T>(_urlNameForSend, objectValue);
        if((int)resultResponce.StatusCode == 200)
        {
            return true;
        }
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
                break;
            case PostsRequests.UpdateProfile:
                return await PostRequest(objectValue, "UpdateProfile");
                break;

            case PostsRequests.CreateEquip:
                return await PostRequest(objectValue, "CreateEquip");
                break;

            case PostsRequests.AddReantils:
                return await PostRequest(objectValue, "AddReantils");
                break;

            case PostsRequests.UpdateReantilsById:
                return await PostRequest(objectValue, "UpdateReantilsById");
                break;

            case PostsRequests.AddPolygon:
                return await PostRequest(objectValue, "AddPolygon");
                break;

            default:
                return false;
                break;
        }
    }
}

