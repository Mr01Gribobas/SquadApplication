using System.Text.Json;

namespace SquadApplication.Repositories.ManagerRequest;

internal class RequestTuple
{
    public RequestTuple(UserModelEntity userSession)
    {
        _httpClient = new HttpClient();
        _userSession = userSession;
    }
    private readonly UserModelEntity _userSession;
    private readonly HttpClient _httpClient;
    private string _urlNameForSend = "http://10.0.2.2:5213/MainGet/";
    public int _currentStatusCode { get; private set; }
    public int GetStatusCode() => _currentStatusCode;
    public void ResetUrl() => _urlNameForSend = "http://10.0.2.2:5213/MainGet/";
    public void UpdateUrl(string url)
    {
        _urlNameForSend += url;
    }
    public async Task<(UserModelEntity, TeamEntity, EquipmentEntity?)> GetAllInfoForUser(UserModelEntity userModel)
    {
        if(userModel is null)
        {
            throw new ArgumentNullException();
        }
        UpdateUrl($"GetAllInfoForProfile?userId={userModel.Id}");
        var responce = await _httpClient.GetAsync(_urlNameForSend);
        if(responce != null && (int)responce.StatusCode == 200)
        {
            try
            {
                (UserModelEntity i1, TeamEntity i2) resultJson = await responce.Content.ReadFromJsonAsync<(UserModelEntity, TeamEntity)>();
                string? result = await responce.Content.ReadAsStringAsync();
                ResetUrl();
                return default;

            }
            catch(Exception ex)
            {
                ResetUrl();
                return (default, default, default);
            }
        }
        ResetUrl();
        return (default, default, default);
    }


}
