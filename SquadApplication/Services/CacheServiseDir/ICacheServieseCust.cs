namespace SquadApplication.Services.CacheServiseDir;

public interface ICacheServieseCust
{
    T GetItemByKey<T>(string key) where T : class;
    void Set<T>(string key,T value) where T : class;
    void Remove(string key);
}