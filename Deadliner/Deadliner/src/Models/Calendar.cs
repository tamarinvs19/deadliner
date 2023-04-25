using Deadliner.Api.Models;
using Deadliner.Utils;

namespace Deadliner.Models;

public class Calendar : ICalendar
{
    public Calendar(int id, IUser user, IEnumerable<ILocalTask> localTasks, IEnumerable<ILocalEvent> localEvents)
    {
        Id = id;
        User = user;
        LocalTasks = localTasks.ToList();
        LocalEvents = localEvents.ToList();
    }
    
    public Calendar(IUser user, IEnumerable<ILocalTask> localTasks, IEnumerable<ILocalEvent> localEvents)
    {
        Id = IdGenerator.Instance.NextId();
        User = user;
        LocalTasks = localTasks.ToList();
        LocalEvents = localEvents.ToList();
    }
    
    public Calendar(IUser user, IEnumerable<ILocalAction> localActions)
    {
        Id = IdGenerator.Instance.NextId();
        User = user;
        var actions = localActions.ToList();
        LocalTasks = actions.OfType<ILocalTask>().ToList();
        LocalEvents = actions.OfType<ILocalEvent>().ToList();
    }

    public override bool Equals(object? obj)
    {
        if (obj is ICalendar other)
        {
            return Equals(other);
        }

        return false;
    }

    protected bool Equals(ICalendar other)
    {
        return Id == other.Id && LocalTasks.Equals(other.LocalTasks) && LocalEvents.Equals(other.LocalEvents);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public int Id { get; }
    public IUser User { get; set; }
    public List<ILocalTask> LocalTasks { get; set; }
    public List<ILocalEvent> LocalEvents { get; set; }
}