using Deadliner.Api.Controller;
using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Api.Storage;
using Deadliner.Storage.EF.DataProviders;
using Deadliner.Storage.EF.ModelsDB;

namespace Deadliner.Controller;

public class EfController<T> : IController<T> where T : IObject
{
    private readonly IStorage<T> _storage;
    private readonly DeadlinerContext _context;

    public EfController()
    {
        _context = new DeadlinerContext();
        _storage = GetProvider<T>();
    }

    private IStorage<TS> GetProvider<TS>() where TS: IObject
    {
        var type = typeof(TS);
        if (type == typeof(IUser))
        {
            return (IStorage<TS>)new UserDataProvider(_context);
        }

        if (type == typeof(ISuperGroup))
        {
            return (IStorage<TS>)new SuperGroupDataProvider(_context);
        }
        if (type == typeof(IGroup))
        {
            return (IStorage<TS>)new GroupDataProvider(_context);
        }
        if (type == typeof(ILocalEvent))
        {
            return (IStorage<TS>)new LocalEventDataProvider(_context);
        }
        if (type == typeof(ILocalTask))
        {
            return (IStorage<TS>)new LocalTaskDataProvider(_context);
        }
        if (type == typeof(IUserToGroup))
        {
            return (IStorage<TS>)new UserToGroupDataProvider(_context);
        }
        if (type == typeof(IUserToLocalAction))
        {
            return (IStorage<TS>)new UserToLocalActionDataProvider(_context);
        }
        if (type == typeof(IUserToSuperGroup))
        {
            return (IStorage<TS>)new UserToSuperGroupDataProvider(_context);
        }

        throw new Exception($"Bad type {typeof(TS)}");
    }

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
        _storage.Save();
    }

    public void Update(T item)
    {
        _storage.Update(item);
        _storage.Save();
    }

    public void Delete(int id)
    {
        _storage.Delete(id);
        _storage.Save();
    }
}