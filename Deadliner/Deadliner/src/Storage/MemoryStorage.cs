using System.ComponentModel.DataAnnotations;
using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Models;

namespace Deadliner.Storage;

public class MemoryStorage<T> : IStorage<T> where T : IObject
{
    private readonly Dictionary<int, T> _storage;

    public MemoryStorage()
    {
        _storage = new Dictionary<int, T>();
    }

    public void Dispose() { }

    public IEnumerable<T> Items()
    {
        return _storage.Values.AsEnumerable();
    }

    public T Get(int id)
    {
        return _storage[id];
    }

    public void Create(T item)
    {
        if (_storage.Keys.Contains(item.Id))
        {
            throw new ValidationException($"Object with id={item.Id} already exists");
        }
        _storage[item.Id] = item;
    }

    public void Update(T item)
    {
        _storage[item.Id] = item;
    }

    public void Delete(int id)
    {
        _storage.Remove(id);
    }

    public void Save() { }
}