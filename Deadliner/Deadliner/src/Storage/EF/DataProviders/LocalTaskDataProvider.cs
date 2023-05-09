using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage.EF.Mappers;
using Deadliner.Storage.EF.ModelsDB;
using Microsoft.EntityFrameworkCore;

namespace Deadliner.Storage.EF.DataProviders;

public class LocalTaskDataProvider : IStorage<ILocalTask>
{
    private readonly DeadlinerContext _context;
    private readonly DbSet<LocalTask> _dbSet;
    private readonly LocalTaskMapper _mapper;
    
    public LocalTaskDataProvider(DeadlinerContext context)
    {
        _context = context;
        _dbSet = _context.LocalTasks;
        _mapper = new LocalTaskMapper();
    }
    
    public void Dispose() { }

    public IEnumerable<ILocalTask> Items()
    {
        return _dbSet.ToList().Select(it => _mapper.ReadItem(it));
    }

    public ILocalTask Get(int id)
    {
        return _dbSet
            .ToList()
            .Where(it => it.Id == id)
            .Select(it => _mapper.ReadItem(it))
            .First();
    }

    public void Create(ILocalTask item)
    {
        var dbItem = _mapper.WriteItem(item);
        _dbSet.Add(dbItem);
    }

    public void Update(ILocalTask item)
    {
        var dbItem = _mapper.WriteItem(item);
        var current = _dbSet.First(it => it.Id == item.Id);
        _context.Entry(current).CurrentValues.SetValues(dbItem);
    }

    public void Delete(int id)
    {
        _dbSet.RemoveRange(_dbSet.Where(it => it.Id == id));
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
