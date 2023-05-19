using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage;

namespace DeadlinerUI.Models;

public class LocalEventsModel
{
    public LocalEventsModel(ICalendar calendar)
    {
        LocalEvents = new MemoryStorage<ILocalEvent>();

        foreach (var localAction in calendar.LocalEvents)
        {
            LocalEvents.Create(localAction);
        }
    }

    public IStorage<ILocalEvent> LocalEvents { get; }
}