
using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage.EF.Mappers;
using Deadliner.Storage.EF.ModelsDB;
using Microsoft.EntityFrameworkCore;

namespace Deadliner.Storage.EF.DataProviders;

public class GroupDataProvider : IStorage<IGroup>
{
    private readonly DeadlinerContext _context;
    private readonly DbSet<Group> _dbSet;
    private readonly GroupMapper _mapper;
    
    public GroupDataProvider(DeadlinerContext context)
    {
        _context = context;
        _dbSet = _context.Groups;
        _mapper = new GroupMapper();
    }
    
    public void Dispose() { }

    public IEnumerable<IGroup> Items()
    {
        return _dbSet.Select(it => _mapper.ReadItem(it));
    }

    public IGroup Get(int id)
    {
        return _dbSet
            .Where(it => it.Id == id)
            .Select(it => _mapper.ReadItem(it))
            .First();
    }

    public void Create(IGroup item)
    {
        var dbItem = _mapper.WriteItem(item);
        _dbSet.Add(dbItem);
    }

    public void Update(IGroup item)
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