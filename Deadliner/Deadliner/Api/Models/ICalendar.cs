namespace Deadliner.Api.Models;

public interface ICalendar : IObject
{
    List<ILocalTask> LocalTasks { get; set; }    
    List<ILocalEvent> LocalEvents { get; set; }
}