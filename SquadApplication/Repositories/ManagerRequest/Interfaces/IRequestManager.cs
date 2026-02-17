using SquadApplication.Repositories.Enums;

namespace SquadApplication.Repositories.ManagerRequest.Interfaces;

public interface IRequestManager<T>
{
    Task<List<T>> GetDataAsync(GetRequests getType);
    Task<bool> PostRequests(T objectValue, PostsRequests postRequests);
    public void SetUrl(string controllAction);
    public void ResetUrlAndStatusCode();
    Task<bool> PutchRequestAsync(PutchRequest getType);

}
