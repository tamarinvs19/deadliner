using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage.EF.Mappers;
using Deadliner.Storage.EF.ModelsDB;
using Microsoft.EntityFrameworkCore;

namespace Deadliner.Storage.EF.DataProviders;

public class LocalEventDataProvider : IStorage<ILocalEvent>
{
    private readonly DeadlinerContext _context;
    private readonly DbSet<LocalEvent> _dbSet;
    private readonly LocalEventMapper _mapper;
    
    public LocalEventDataProvider(DeadlinerContext context)
    {
        _context = context;
        _dbSet = _context.LocalEvents;
        _mapper = new LocalEventMapper();
    }
    
    public void Dispose() { }

    public IEnumerable<ILocalEvent> Items()
    {
        return _dbSet.Select(it => _mapper.ReadItem(it));
    }

    public ILocalEvent Get(int id)
    {
        return _dbSet
            .Where(it => it.Id == id)
            .Select(it => _mapper.ReadItem(it))
            .First();
    }

    public void Create(ILocalEvent item)
    {
        var dbItem = _mapper.WriteItem(item);
        _dbSet.Add(dbItem);
    }

    public void Update(ILocalEvent item)
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