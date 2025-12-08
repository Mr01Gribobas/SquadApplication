using SquadApplication.Repositories.Enums;

namespace SquadApplication.Repositories.Interfaces;

public interface IRequestManager<T>
{
    Task<List<T>> GetData(GetRequests getType);
    Task<List<T>> PostRequests(PostsRequests postRequests);
    public void SetUrl(string controllAction);
}
