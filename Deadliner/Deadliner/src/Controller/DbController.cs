using Deadliner.Api.Controller;
using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Api.Storage;
using Deadliner.Storage.Ado.DataProviders;
using Deadliner.Storage.EF.DataProviders;

namespace Deadliner.Controller;

public class DbController<T> : IController<T> where T : IObject
{
    private readonly IStorage<T> _storage;

    public DbController()
    {
        _storage = GetProvider<T>();
    }

    private static IStorage<TS> GetProvider<TS>() where TS : IObject
    {
        var type = typeof(TS);
        if (type == typeof(IUser))
        {
            return (IStorage<TS>)new UserDataProvider();
        }

        if (type == typeof(ISuperGroup))
        {
            return (IStorage<TS>)new SuperGroupDataProvider();
        }
        if (type == typeof(IGroup))
        {
            return (IStorage<TS>)new GroupDataProvider();
        }
        if (type == typeof(ILocalEvent))
        {
            return (IStorage<TS>)new LocalEventDataProvider();
        }
        if (type == typeof(ILocalTask))
        {
            return (IStorage<TS>)new LocalTaskDataProvider();
        }
        if (type == typeof(IUserToGroup))
        {
            return (IStorage<TS>)new UserToGroupDataProvider();
        }
        if (type == typeof(IUserToLocalAction))
        {
            return (IStorage<TS>)new UserToLocalActionDataProvider();
        }
        if (type == typeof(IUserToSuperGroup))
        {
            return (IStorage<TS>)new UserToSuperGroupDataProvider();
        }
        if (type == typeof(ICalendar))
        {
            return (IStorage<TS>)new CalendarDataProvider(new EfContext());
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
        _storage.Save();
    }
}