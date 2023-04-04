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
            .Where(it => it.User == User && it.Group == group)
            .Select(it => it.Group);
        var actions = (_context.LocalEvents.Items().Union<ILocalAction>(_context.LocalTasks.Items()))
            .Where(it => groupsWithUser.Contains(it.Group));
        _localActions = _localActions.Union(actions);

        return this;
    }

    public IAbstractCalendarBuilder AddLocalAction(ILocalAction localAction)
    {
        var actions = _context.UserToLocalAction
            .Items()
            .Where(it => it.User == User && it.LocalAction == localAction)
            .Select(it => it.LocalAction);
        _localActions = _localActions.Union(actions);

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
}