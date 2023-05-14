using Deadliner.Api.Models;
using Deadliner.Utils;

namespace Deadliner.Models;

public class Calendar : ICalendar
{
    public Calendar(IEnumerable<ILocalTask> localTasks, IEnumerable<ILocalEvent> localEvents)
    {
        Id = IdGenerator.Instance.NextId();
        LocalTasks = localTasks.ToList();
        LocalEvents = localEvents.ToList();
    }
    
    public Calendar(IEnumerable<ILocalAction> localActions)
    {
        Id = IdGenerator.Instance.NextId();
        var actions = localActions.ToList();
        LocalTasks = actions.OfType<ILocalTask>().ToList();
        LocalEvents = actions.OfType<ILocalEvent>().ToList();
    }

    public int Size()
    {
        return LocalTasks.Count + LocalEvents.Count;
    }


    public DateTime? DateTime { get; set; }

    public List<ILocalAction> LocalActions()
    {
        var res = LocalEvents.Cast<ILocalAction>().ToList();
        res.AddRange(LocalTasks.Cast<ILocalAction>().ToList());
        return res;
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
        return HashCode.Combine(Id, LocalTasks, LocalEvents);
    }

    public int Id { get; }
    public List<ILocalTask> LocalTasks { get; set; }
    public List<ILocalEvent> LocalEvents { get; set; }
}