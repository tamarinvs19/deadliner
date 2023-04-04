using Deadliner.Api.Models;
using Deadliner.Models;

namespace Deadliner.Api.Storage;

public interface IStorage<T> : IDisposable where T : IObject
{
    IEnumerable<T> Items();
    T Get(int id);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
    void Save();
}