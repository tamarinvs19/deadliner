using Deadliner.Api.Models.Relationships;
using Deadliner.Api.Storage;
using Deadliner.Storage.EF.Mappers;
using Deadliner.Storage.EF.ModelsDB;
using Microsoft.EntityFrameworkCore;

namespace Deadliner.Storage.EF.DataProviders;

public class UserToGroupDataProvider : IStorage<IUserToGroup>
{
    private readonly DeadlinerContext _context;
    private readonly DbSet<UserToGroup> _dbSet;
    private readonly UserToGroupMapper _mapper;
    
    public UserToGroupDataProvider(DeadlinerContext context)
    {
        _context = context;
        _dbSet = _context.UserToGroups;
        _mapper = new UserToGroupMapper();
    }
    
    public void Dispose() { }

    public IEnumerable<IUserToGroup> Items()
    {
        return _dbSet.Select(it => _mapper.ReadItem(it));
    }

    public IUserToGroup Get(int id)
    {
        return _dbSet
            .Where(it => it.Id == id)
            .Select(it => _mapper.ReadItem(it))
            .First();
    }

    public void Create(IUserToGroup item)
    {
        var dbItem = _mapper.WriteItem(item);
        _dbSet.Add(dbItem);
    }

    public void Update(IUserToGroup item)
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
