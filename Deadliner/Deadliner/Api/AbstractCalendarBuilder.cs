using Deadliner.Api.Models;

namespace Deadliner.Api;

public interface IAbstractCalendarBuilder  // Строитель
{
    IUser User { get; }
    IAbstractCalendarBuilder AddSuperGroup(ISuperGroup superGroup);
    IAbstractCalendarBuilder AddGroup(IGroup group);
    IAbstractCalendarBuilder AddLocalAction(ILocalAction localAction);
    IAbstractCalendarBuilder FilterParentsLocalAction();
    IAbstractCalendarBuilder FilterKindLocalAction<T> () where T : ILocalAction;
    ICalendar Build();
    public List<ICalendar> DaySplitBuild(DateTime from, DateTime to);
}