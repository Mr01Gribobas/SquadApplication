using SquadApplication.DTO_Classes;
namespace SquadApplication.Repositories.ManagerRequest;

internal class RequestTuple
{
    public RequestTuple(UserModelEntity userSession, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _userSession = userSession;
    }
    private readonly UserModelEntity _userSession;
    private readonly HttpClient _httpClient;
    private string _urlNameForSend = "http://10.0.2.2:5213/";
    public int _currentStatusCode { get; private set; }
    public int GetStatusCode() => _currentStatusCode;
    public void ResetUrlAndStatusCode()
    {
        _urlNameForSend = "http://10.0.2.2:5213/";
        _currentStatusCode = 0;
    }
    public void UpdateUrl(string url) => _urlNameForSend += url;
    public async Task<(UserModelEntity, TeamEntity, EquipmentDTO?)> GetAllInfoForUser(UserModelEntity userModel)
    {
        try
        {
            if(userModel is null)
                throw new ArgumentNullException();
            UpdateUrl($"api/users/GetAllInfoForHome?userId={userModel.Id}");
            var responce = await _httpClient.GetAsync(_urlNameForSend);
            if(responce != null && (int)responce.StatusCode == 200)
            {
                TripleContainerDTO<UserModelEntity, TeamEntity, EquipmentDTO>? resultJson = await responce.Content.ReadFromJsonAsync<TripleContainerDTO<UserModelEntity, TeamEntity, EquipmentDTO>>();
                (UserModelEntity, TeamEntity, EquipmentDTO) typle = (resultJson._itemOne, resultJson._itemTwo, resultJson._itemThree);
                ResetUrlAndStatusCode();
                return typle;
            }
            else
                throw new Exception("null responce");
        }
        catch(Exception ex)
        {
            ResetUrlAndStatusCode();
            return (default, default, default);
        }
    }
}