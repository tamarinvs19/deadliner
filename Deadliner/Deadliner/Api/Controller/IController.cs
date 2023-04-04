using Deadliner.Api.Models;
using Deadliner.Models;

namespace Deadliner.Api.Controller;

public interface IController<T> where T: IObject
{
    IEnumerable<T> Items();
    T Get(int id);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
}