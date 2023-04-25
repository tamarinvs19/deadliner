using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Api.Utils;
using Deadliner.Controller;
using Deadliner.Models;
using Deadliner.Utils;

namespace Deadliner.Api.Controller;

public interface IContext  // фасад
{
    IController<ISuperGroup> SuperGroups { get; }
    IController<IGroup> Groups { get; }
    IController<IUser> Users { get; }
    IController<ILocalTask> LocalTasks { get; }
    IController<ILocalEvent> LocalEvents { get; }
    IController<IUserToSuperGroup> UserToSuperGroup { get; }
    IController<IUserToGroup> UserToGroup { get; }
    IController<IUserToLocalAction> UserToLocalAction { get; }
    IController<ICalendar> Calendars { get; }
    ITimeProvider TimeProvider { get; }
}