using SquadApplication.Repositories.Enums;

namespace SquadApplication.Repositories.Interfaces;

public interface IRequestManager<T>
{
    Task<List<T>> GetData(GetRequests getType);
    Task<bool> PostRequests(T objectValue, PostsRequests postRequests);
    public void SetUrl(string controllAction);
}
