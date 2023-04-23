using Deadliner.Api.Controller;
using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Api.Utils;
using Deadliner.Utils;

namespace Deadliner.Controller;

public class AdoContext : IContext
{
    public AdoContext()
    {
        SuperGroups = new AdoController<ISuperGroup>();
        Groups = new AdoController<IGroup>();
        Users = new AdoController<IUser>();
        LocalTasks = new AdoController<ILocalTask>();
        LocalEvents = new AdoController<ILocalEvent>();
        UserToSuperGroup = new AdoController<IUserToSuperGroup>();
        UserToGroup = new AdoController<IUserToGroup>();
        UserToLocalAction = new AdoController<IUserToLocalAction>();
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