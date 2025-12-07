using SquadApplication.Repositories.Interfaces;

namespace SquadApplication.Repositories;

public class ManagerRequestApplication<T> : IRequestManager<T>
{
    public ManagerRequestApplication()
    {
        _httpClient = new HttpClient();
    }
    private HttpClient _httpClient;
    private string _urlNameForSend = "https://localhost:7176/";
    public int _currentStatusCode { get; private set; }

    public Task<List<T>> GetData()
    {
        throw new NotImplementedException();
    }
}
