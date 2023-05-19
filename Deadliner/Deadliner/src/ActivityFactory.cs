using Deadliner.Api;
using Deadliner.Api.Controller;
using Deadliner.Api.Models;
using Deadliner.Api.Models.LocalActionStates;
using Deadliner.Api.Utils;
using Deadliner.Models;
using Deadliner.Models.LocalActionStates;

namespace Deadliner;

public class ActivityFactory : IAbstractActivityFactory
{
    private IContext _context;
    private readonly IIdGenerator _idGenerator;

    public ActivityFactory()
    {
        _context = MainContainer.Context();
        _idGenerator = MainContainer.IdGenerator();
    }
    
    public ActivityFactory(IContext context)
    {
        _context = context;
        _idGenerator = MainContainer.IdGenerator();
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

        var id = _idGenerator.NextId();
        var localEvent = new LocalEvent(id, title, description, datetime, group, state, null);
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

        var id = _idGenerator.NextId();
        var localTask = new LocalTask(id, title, description, creationTime, deadline, group, state, null);
        _context.LocalTasks.Create(localTask);
        return localTask;
    }

    public T AddChild<T>(T parentAction, T localAction) where T : ILocalAction
    {
        localAction.Parent = parentAction;
        return parentAction;
    }
}