using Deadliner.Controller;
using Deadliner.Models;
using Deadliner.Models.LocalActionStates;

namespace Deadliner;

public class ActivityFactory : IAbstractActivityFactory
{
    private IContext _context;
    
    public ActivityFactory()
    {
        _context = MainContainer.Context();
    }
    
    public ActivityFactory(IContext context)
    {
        _context = context;
    }

    private DateTime CurrentDateTime => _context.TimeProvider.Now();

    public ILocalEvent MakeLocalEvent(string title, string description, DateTime datetime, IGroup group)
    {
        ILocalActionState state;
        if (datetime < CurrentDateTime)
        {
            state = new OverdueState();
        }
        else
        {
            state = new FutureState();
        }

        var localEvent = new LocalEvent(title, description, datetime, group, state);
        _context.LocalEvents.Create(localEvent);
        return localEvent;
    }

    public ILocalTask MakeTask(string title, string description, DateTime creationTime, DateTime deadline, IGroup group)
    {
        if (creationTime > deadline)
        {
            throw new ArgumentException($"creationTime {creationTime} should be more than deadline {deadline}.");
        }

        ILocalActionState state;
        if (CurrentDateTime < creationTime)
        {
            state = new FutureState();
        }
        else if (CurrentDateTime <= deadline)
        {
            state = new ActualState();
        }
        else
        {
            state = new OverdueState();
        }

        var localTask = new LocalTask(title, description, creationTime, deadline, group, state);
        _context.LocalTasks.Create(localTask);
        return localTask;
    }

    public T AddChild<T>(T parentAction, T localAction) where T : ILocalAction
    {
        localAction.Parent = parentAction;
        return parentAction;
    }
}