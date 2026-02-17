using SquadApplication.Repositories.ManagerRequest.Interfaces;

namespace SquadApplication.Repositories.ManagerRequest;

public class ManagerGetRequests<T> : IRequestManager<T>, IDisposable
    where T : class
{
    public ManagerGetRequests()
    {
        _httpClient = new HttpClient();
    }
    private HttpClient _httpClient;
    public string _urlNameForSend { get; private set; } = "http://10.0.2.2:5213/MainGet/";
    public int _currentStatusCode { get; private set; }
    private bool _disposed = false;

    public void ResetUrlAndStatusCode()
    {
        _urlNameForSend = "http://10.0.2.2:5213/MainGet/";
        _currentStatusCode = 0;
    }


    public void SetUrl(string controllAction)
    {
        _urlNameForSend += controllAction;
    }
    public static async Task<UserModelEntity?> GetUserById(int id)
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("http://10.0.2.2:5213");
        var responce = await client.GetAsync($"/MainGet/GetUserById?Id={id}");
        if((int)responce.StatusCode == 401)
        {
            return null;
        }
        try
        {
            return await responce.Content.ReadFromJsonAsync<UserModelEntity>();
        }
        catch(Exception ex)
        {
            return null;
        }
    }





    public async Task<List<T>?> GetDataAsync(GetRequests getRequessts)
    {
        switch(getRequessts)
        {
            case GetRequests.GetAllTeamMembers:
                (bool flowControlAllTeam, List<T>? valueAllTeam) = await RequestAction();
                if(!flowControlAllTeam)
                {
                    return valueAllTeam;
                }
                break;
            case GetRequests.GetAllReantil:
                (bool flowControlAllReantil, List<T>? valueAllReantil) = await RequestAction();
                if(!flowControlAllReantil)
                {
                    return valueAllReantil;
                }
                break;
            case GetRequests.GetAllPolygons:
                (bool flowControlAllPolygons, List<T>? valueAllPolygons) = await RequestAction();
                if(!flowControlAllPolygons)
                {
                    return valueAllPolygons;
                }
                break;
            case GetRequests.GetAllEventsHistory:
                (bool flowControlAllEventsHistory, List<T>? valueAllEventsHistory) = await RequestAction();
                if(!flowControlAllEventsHistory)
                {
                    return valueAllEventsHistory;
                }
                break;
            case GetRequests.GetEvent:
                (bool flowControlEvent, List<T>? valueEvent) = await RequestAction();
                if(!flowControlEvent)
                {
                    return valueEvent;
                }
                break;
            case GetRequests.GetEquipById:

                (bool flowControlEquipById, List<T>? valueEquipById) = await RequestAction();
                if(!flowControlEquipById)
                {
                    return valueEquipById;
                }
                break;
            case GetRequests.GameAttendance:

                (bool flowControlGameAttendance, List<T>? objects) = await RequestAction();
                if(!flowControlGameAttendance)
                {
                    return objects;
                }
                break;                

             case GetRequests.AllInfoForProfile:

                (bool flowAllInfo, List<T>? statistics)  = await RequestAction();
                if(!flowAllInfo)
                {
                    return statistics;
                }
                break;
            default:
                return null;
        }
        return null;
    }

    private async Task<(bool flowControl, List<T>? value)> RequestAction()
    {
        var responce = await _httpClient.GetAsync(_urlNameForSend);
        _currentStatusCode = (int)responce.StatusCode;
        if((int)responce.StatusCode == 200)
        {
            var dataFromResponce = await responce.Content.ReadFromJsonAsync<List<T>>();
            return (flowControl: false, value: dataFromResponce);
        }
        return (flowControl: true, value: null);
    }
    public async Task<bool> PutchRequestAsync(PutchRequest getType)
    {
        switch(getType)
        {
            case PutchRequest.UpdateRank:
                 var responce = await _httpClient.GetFromJsonAsync<bool>(_urlNameForSend);
                return responce;
            default:
                return false;
        }
    }

    public Task<bool> PostRequests(T objectValue, PostsRequests postRequests)
    {
        throw new NotImplementedException();
    }



    ~ManagerGetRequests()
    {
        Dispose(false);
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if(!_disposed)
        {
            if(disposing)
            {
                _httpClient?.Dispose();
            }
            _disposed = true;
        }
    }

    
}
