using Deadliner.Api;
using Deadliner.Api.Controller;
using Deadliner.Api.Models;
using Deadliner.Controller;
using Deadliner.Models;

namespace Deadliner;

public class CalendarBuilder : IAbstractCalendarBuilder
{
    private IEnumerable<ILocalAction> _localActions;
    private readonly IContext _context;

    public CalendarBuilder(IContext context, IUser user)
    {
        _context = context;
        User = user;
        _localActions = new List<ILocalAction>();
    }
    
    public IUser User { get; }

    public IAbstractCalendarBuilder AddSuperGroup(ISuperGroup superGroup)
    {
        var superGroupsWithUser = _context.UserToSuperGroup
            .Items()
            .Where(it => it.User == User && it.SuperGroup == superGroup)
            .ToList()
            .Count;
        if (superGroupsWithUser > 0)
        {
            var groupsWithUser = _context.UserToGroup
                .Items()
                .Where(it => it.User == User && it.Group.SuperGroup == superGroup)
                .Select(it => it.Group);
            var actions = (_context.LocalEvents.Items().Union<ILocalAction>(_context.LocalTasks.Items()))
                .Where(it => groupsWithUser.Contains(it.Group));
            _localActions = _localActions.Union(actions);
        }

        return this;
    }

    public IAbstractCalendarBuilder AddGroup(IGroup group)
    {
        var groupsWithUser = _context.UserToGroup
            .Items()
            .Where(it => Equals(it.User, User) && Equals(it.Group, group))
            .Select(it => it.Group);
        var actions = (_context.LocalEvents.Items().Union<ILocalAction>(_context.LocalTasks.Items()))
            .Where(it => groupsWithUser.Contains(it.Group));
        _localActions = _localActions.Union(actions);

        return this;
    }

    public IAbstractCalendarBuilder AddLocalAction(ILocalAction localAction)
    {
        var actionsIds = _context.UserToLocalAction
            .Items()
            .Where(it => it.User.Id == User.Id && it.LocalAction.Id == localAction.Id)
            .Select(it => it.LocalAction.Id);
        if (actionsIds.Contains(localAction.Id))
        {
            _localActions = _localActions.Append(localAction);
        }

        return this;
    }

    public IAbstractCalendarBuilder FilterParentsLocalAction()
    {
        _localActions = _localActions.Where(it => it.Parent == null);
        return this;
    }

    public IAbstractCalendarBuilder FilterKindLocalAction<T>() where T : ILocalAction
    {
        _localActions = _localActions.Where(it => it is T);
        return this;
    }

    public ICalendar Build()
    {
        return new Calendar(_localActions);
    }
    
    public List<ICalendar> DaySplitBuild(DateTime from, DateTime to)
    {
        var calendars = new List<ICalendar>();
        var dayCount = (to - from).Days;
        for (var i = 0; i < dayCount; i++)
        {
            var dt = from + TimeSpan.FromDays(i);
            var calendar = new Calendar(_localActions.Where(it => IsLocalActionInDate(dt, it)))
            {
                DateTime = dt
            };
            calendars.Add(calendar);
        }

        return calendars;
    }

    private static bool IsLocalActionInDate(DateTime dateTime, ILocalAction localAction)
    {
        return localAction switch
        {
            ILocalEvent localEvent => localEvent.DateTime.Date == dateTime.Date,
            ILocalTask localTask => localTask.Deadline >= dateTime && localTask.CreationDateTime <= dateTime,
            _ => false
        };
    }
}