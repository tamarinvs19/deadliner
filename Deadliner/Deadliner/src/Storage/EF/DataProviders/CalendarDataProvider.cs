using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Controller;

namespace Deadliner.Storage.EF.DataProviders;

public class CalendarDataProvider : IStorage<ICalendar>
{
    private readonly EfContext _dbContext;

    public CalendarDataProvider(EfContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Dispose()
    {
    }

    public IEnumerable<ICalendar> Items()
    {
        return _dbContext.Calendars.Select(it => it.ToCalendar());
    }

    public ICalendar Get(int id)
    {
        return _dbContext.Calendars.Find(id).ToCalendar();
    }

    public void Create(ICalendar item)
    {
        _dbContext.Calendars.Add(new DbCalendar(item));
    }

    public void Update(ICalendar item)
    {
        _dbContext.Calendars.Update(new DbCalendar(item));
    }

    public void Delete(int id)
    {
        using var transaction = _dbContext.Database.BeginTransaction();

        try
        {
            var calendar = _dbContext.Calendars.Find(id);
            if (calendar != null)
            {
                _dbContext.Calendars.Remove(calendar);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
        }
    }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}