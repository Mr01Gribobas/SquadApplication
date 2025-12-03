using SquadApplication.Models.EntityModels;

namespace SquadApplication.Repositories;

public class DataBaseManager
{
    public DataBaseManager()
    {
        _httpClient = new HttpClient();
    }
    private HttpClient _httpClient;
    private string urlNameForSend = "";
    public bool SendDataForEnter(DataFor dataFor,object data)
    {
        if(dataFor==DataFor.Registration)
        {
            UserModel user = (UserModel)data;
        }
        else
        {
            int number = int.Parse((string)data);
        }

            return default(bool);
    }
}
    
