using System.Globalization;
using Deadliner.Api.Controller;
using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Api.Utils;
using Deadliner.Storage.EF.DataProviders;
using Deadliner.Utils;

namespace Deadliner.Controller;

public class DbContext : IContext
{
    public DbContext()
    {
        SuperGroups = new DbController<ISuperGroup>();
        Groups = new DbController<IGroup>();
        Users = new DbController<IUser>();
        LocalTasks = new DbController<ILocalTask>();
        LocalEvents = new DbController<ILocalEvent>();
        UserToSuperGroup = new DbController<IUserToSuperGroup>();
        UserToGroup = new DbController<IUserToGroup>();
        UserToLocalAction = new DbController<IUserToLocalAction>();
        Calendars = new DbController<ICalendar>();
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
    public IController<ICalendar> Calendars { get; }
    public ITimeProvider TimeProvider { get; }
}