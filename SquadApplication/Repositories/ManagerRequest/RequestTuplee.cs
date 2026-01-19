using SquadApplication.DTO_Classes;
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
    public void ResetUrlAndStatusCode() 
    {
        _urlNameForSend = "http://10.0.2.2:5213/MainGet/"; 
        _currentStatusCode = 0;
    }
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
            _currentStatusCode = 200; 
            try
            {
                TripleContainerDTO<UserModelEntity, TeamEntity, EquipmentEntity> resultJson = await responce.Content.ReadFromJsonAsync<TripleContainerDTO<UserModelEntity, TeamEntity, EquipmentEntity>>();
                (UserModelEntity, TeamEntity, EquipmentEntity) typle = (resultJson._itemOne,resultJson._itemTwo,resultJson._itemThree);
                ResetUrlAndStatusCode();
                return typle;

            }
            catch(Exception ex)
            {
                ResetUrlAndStatusCode();
                return (default, default, default);
            }
        }
        ResetUrlAndStatusCode();
        return (default, default, default);
    }


}
