using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage;

namespace DeadlinerUI.Models;

public class LocalActionsModel
{
    public LocalActionsModel(Deadliner.Models.Calendar calendar)
    {
        LocalTasks = new MemoryStorage<ILocalTask>();
        LocalEvents = new MemoryStorage<ILocalEvent>();

        foreach (var localAction in calendar.LocalTasks)
        {
            LocalTasks.Create(localAction);
        }
        foreach (var localAction in calendar.LocalEvents)
        {
            LocalEvents.Create(localAction);
        }
    }

    public IStorage<ILocalTask> LocalTasks { get; }
    public IStorage<ILocalEvent> LocalEvents { get; }
}