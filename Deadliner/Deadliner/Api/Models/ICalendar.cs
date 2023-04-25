namespace Deadliner.Api.Models;

public interface ICalendar : IObject
{
    IUser User { get; set; }
    List<ILocalTask> LocalTasks { get; set; }    
    List<ILocalEvent> LocalEvents { get; set; }
}