using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage.EF.Mappers;
using Deadliner.Storage.EF.ModelsDB;
using Microsoft.EntityFrameworkCore;

namespace Deadliner.Storage.EF.DataProviders;

public class UserDataProvider : IStorage<IUser>
{
    private readonly DeadlinerContext _context;
    private readonly DbSet<User> _dbSet;
    private readonly UserMapper _mapper;
    
    public UserDataProvider(DeadlinerContext context)
    {
        _context = context;
        _dbSet = _context.Users;
        _mapper = new UserMapper();
    }
    
    public void Dispose() { }

    public IEnumerable<IUser> Items()
    {
        return _dbSet.Select(it => _mapper.ReadItem(it));
    }

    public IUser Get(int id)
    {
        return _dbSet
            .Where(it => it.Id == id)
            .Select(it => _mapper.ReadItem(it))
            .First();
    }

    public void Create(IUser item)
    {
        var dbItem = _mapper.WriteItem(item);
        _dbSet.Add(dbItem);
    }

    public void Update(IUser item)
    {
        var dbItem = _mapper.WriteItem(item);
        var current = _dbSet.First(it => it.Id == item.Id);
        _context.Entry(current).CurrentValues.SetValues(dbItem);
    }

    public void Delete(int id)
    {
        foreach (var item in _dbSet.Where(it => it.Id == id))
        {
            _dbSet.Remove(item);
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
