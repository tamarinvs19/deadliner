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

    public int Id { get; }
    public List<ILocalTask> LocalTasks { get; set; }
    public List<ILocalEvent> LocalEvents { get; set; }
}