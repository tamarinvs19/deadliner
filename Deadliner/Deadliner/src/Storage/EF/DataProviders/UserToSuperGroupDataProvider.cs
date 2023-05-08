using Deadliner.Api.Models.Relationships;
using Deadliner.Api.Storage;
using Deadliner.Storage.EF.Mappers;
using Deadliner.Storage.EF.ModelsDB;
using Microsoft.EntityFrameworkCore;

namespace Deadliner.Storage.EF.DataProviders;

public class UserToSuperGroupDataProvider : IStorage<IUserToSuperGroup>
{
    private readonly DeadlinerContext _context;
    private readonly DbSet<UserToSuperGroup> _dbSet;
    private readonly UserToSuperGroupMapper _mapper;
    
    public UserToSuperGroupDataProvider(DeadlinerContext context)
    {
        _context = context;
        _dbSet = _context.UserToSuperGroups;
        _mapper = new UserToSuperGroupMapper();
    }
    
    public void Dispose() { }

    public IEnumerable<IUserToSuperGroup> Items()
    {
        return _dbSet.Select(it => _mapper.ReadItem(it));
    }

    public IUserToSuperGroup Get(int id)
    {
        return _dbSet
            .Where(it => it.Id == id)
            .Select(it => _mapper.ReadItem(it))
            .First();
    }

    public void Create(IUserToSuperGroup item)
    {
        var dbItem = _mapper.WriteItem(item);
        _dbSet.Add(dbItem);
    }

    public void Update(IUserToSuperGroup item)
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
