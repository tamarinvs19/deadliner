using System.ComponentModel.DataAnnotations.Schema;
using Deadliner.Api.Models;
using Deadliner.Models;

namespace Deadliner.Storage.EF.DataProviders;

[Table("Calendars")]
public class DbCalendar
{
    public int Id { get; set; }
    public User User { get; set; }
    public List<LocalTask> LocalTasks { get; set; }
    public List<LocalEvent> LocalEvents { get; set; }

    public Calendar ToCalendar()
    {
        return new Calendar(Id, User, LocalTasks, LocalEvents);
    }

    public DbCalendar(int id, User user, List<LocalTask> tasks, List<LocalEvent> events)
    {
        Id = id;
        User = user;
        LocalTasks = tasks;
        LocalEvents = events;
    }
        
    public DbCalendar(ICalendar calendar)
    {
        Id = calendar.Id;
        User = new User { Id = calendar.User.Id, Username = calendar.User.Username, Password = calendar.User.Password };
        LocalTasks = calendar.LocalTasks.Select(it => new LocalTask(
            it.Id, it.Title, it.Description, it.CreationDateTime, it.Deadline, it.Group, it.State, it.Parent
        )).ToList();
        LocalEvents = calendar.LocalEvents.Select(it => new LocalEvent(
            it.Id, it.Title, it.Description, it.DateTime, it.Group, it.State, it.Parent
        )).ToList();
    }
}