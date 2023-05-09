using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage.EF.Mappers;
using Deadliner.Storage.EF.ModelsDB;
using Microsoft.EntityFrameworkCore;

namespace Deadliner.Storage.EF.DataProviders;

public class SuperGroupDataProvider : IStorage<ISuperGroup>
{
    private readonly DeadlinerContext _context;
    private readonly DbSet<SuperGroup> _dbSet;
    private readonly SuperGroupMapper _mapper;
    
    public SuperGroupDataProvider(DeadlinerContext context)
    {
        _context = context;
        _dbSet = _context.SuperGroups;
        _mapper = new SuperGroupMapper();
    }
    
    public void Dispose() { }

    public IEnumerable<ISuperGroup> Items()
    {
        return _dbSet.ToList().Select(it => _mapper.ReadItem(it));
    }

    public ISuperGroup Get(int id)
    {
        return _dbSet
            .ToList()
            .Where(it => it.Id == id)
            .Select(it => _mapper.ReadItem(it))
            .First();
    }

    public void Create(ISuperGroup item)
    {
        var dbItem = _mapper.WriteItem(item);
        _dbSet.Add(dbItem);
    }

    public void Update(ISuperGroup item)
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
