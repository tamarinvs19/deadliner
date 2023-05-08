using Deadliner.Api.Controller;
using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Api.Utils;
using Deadliner.Utils;

namespace Deadliner.Controller;

public class MyDeadlinerContext : IContext
{
    public MyDeadlinerContext()
    {
        SuperGroups = new EfController<ISuperGroup>();
        Groups = new EfController<IGroup>();
        Users = new EfController<IUser>();
        LocalTasks = new EfController<ILocalTask>();
        LocalEvents = new EfController<ILocalEvent>();
        UserToSuperGroup = new EfController<IUserToSuperGroup>();
        UserToGroup = new EfController<IUserToGroup>();
        UserToLocalAction = new EfController<IUserToLocalAction>();
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