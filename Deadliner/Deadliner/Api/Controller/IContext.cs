using Deadliner.Models;
using Deadliner.Utils;

namespace Deadliner.Controller;

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
    ITimeProvider TimeProvider { get; }
}