using Deadliner.Api.Controller;
using Deadliner.Api.Models;
using Deadliner.Storage;

namespace Deadliner.Controller;

public class GenericController<T> : IController<T> where T : IObject
{
    private readonly MemoryStorage<T> _storage = new ();
    
    public IEnumerable<T> Items()
    {
        return _storage.Items();
    }

    public T Get(int id)
    {
        return _storage.Get(id);
    }

    public void Create(T item)
    {
        _storage.Create(item);
    }

    public void Update(T item)
    {
        _storage.Update(item);
    }

    public void Delete(int id)
    {
        _storage.Delete(id);
    }

    public void Save()
    {
    }
}