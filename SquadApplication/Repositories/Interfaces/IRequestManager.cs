namespace SquadApplication.Repositories.Interfaces;

public interface IRequestManager<T>
{
    Task<List<T>> GetData();
}
