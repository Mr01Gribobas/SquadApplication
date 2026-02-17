namespace SquadApplication.Services.CacheServiseDir;

public class CacheServieseCust : ICacheServieseCust
{
    private Dictionary<string, object> _cache = new Dictionary<string, object>();

    public T? GetItemByKey<T>(string key) where T : class =>  _cache.TryGetValue(key, out var value)
                                                                                ? value as T : null;
    public void Remove(string key) => _cache.Remove(key);
    public void Set<T>(string key, T value) where T : class => _cache[key] = value;

}
