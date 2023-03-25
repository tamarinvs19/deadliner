using Deadliner.Models;
using Deadliner.Utils;

namespace Deadliner.Controller;

public class BaseContext : IContext
{
    public BaseContext()
    {
        SuperGroups = new GenericController<ISuperGroup>();
        Groups = new GenericController<IGroup>();
        Users = new GenericController<IUser>();
        LocalTasks = new GenericController<ILocalTask>();
        LocalEvents = new GenericController<ILocalEvent>();
        UserToSuperGroup = new GenericController<IUserToSuperGroup>();
        UserToGroup = new GenericController<IUserToGroup>();
        UserToLocalAction = new GenericController<IUserToLocalAction>();
        TimeProvider = new TimeProvider();
    }

    public IController<ISuperGroup> SuperGroups { get; }
    public IController<IGroup> Groups { get; }
    public IController<IUser> Users { get; }
    public IController<ILocalTask> LocalTasks { get; }
    public IController<ILocalEvent> LocalEvents { get; }
    public IController<IUserToSuperGroup> UserToSuperGroup { get; }
    public IController<IUserToGroup> UserToGroup { get; }
    public IController<IUserToLocalAction> UserToLocalAction { get; }
    public ITimeProvider TimeProvider { get; }
}