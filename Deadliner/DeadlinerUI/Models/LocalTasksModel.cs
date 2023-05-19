using Deadliner.Api.Controller;
using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Controller;
using Deadliner.Storage;

namespace DeadlinerUI.Models;

public class LocalTasksModel
{
    private readonly IContext _context;
    
    public LocalTasksModel(ICalendar calendar, IContext context)
    {
        _context = context;
        
        LocalTasks = new MemoryStorage<ILocalTask>();

        foreach (var localAction in calendar.LocalTasks)
        {
            LocalTasks.Create(localAction);
        }
    }

    public IStorage<ILocalTask> LocalTasks { get; }
    
}