namespace SquadApplication.Repositories.ManagerRequest;

internal class RequestTuplee<T, T2, T3>
{
    public RequestTuplee(IUserSession userSession)
    {
        _httpClient = new HttpClient();
        _userSession = userSession;
    }
    private readonly IUserSession _userSession;
    private readonly HttpClient _httpClient;
    private string _urlNameForSend = "http://10.0.2.2:5213/MainGet/";
    public int _currentStatusCode { get; private set; }
    public int GetStatusCode() => _currentStatusCode;
    public void UpdateUrl(string url)
    {
        _urlNameForSend += url;
    }
    public async Task<(T?, T2?, T3?)> GetAllInfoForUser(T object1, T2 object2, T3 object3)
    {
        if(object1 is null | object2 is null | object3 is null)
        {
            throw new ArgumentNullException();
        }
        UpdateUrl("GetAllInfoForProfile");
        var responce = await _httpClient.GetAsync(_urlNameForSend);
        if(responce != null && (int)responce.StatusCode == 200)
        {
            try
            {
                (T, T2, T3) result = await responce.Content.ReadFromJsonAsync<(T, T2, T3)>();
                
                return result;

            }
            catch(Exception)
            {
                return (default, default, default);
            }
        }else
        {
            return (default, default, default); 
        }
    }


}
