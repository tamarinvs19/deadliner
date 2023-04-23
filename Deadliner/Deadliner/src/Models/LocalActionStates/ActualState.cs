using Deadliner.Api.Models.LocalActionStates;
using Deadliner.Api.Utils;
using Deadliner.Utils;

namespace Deadliner.Models.LocalActionStates;

public class ActualState : ILocalActionState
{
    private ListOfTypes<ILocalActionState> _neigbours = new(){ typeof(OverdueState) };
    public ListOfTypes<ILocalActionState> Neighbours
    {
        get => _neigbours;
    }
    public Func<ILocalActionState?> Handle()
    {
        throw new NotImplementedException();
    }

    public override bool Equals(object? obj)
    {
        if (obj is ILocalActionState other)
        {
            return Equals(other);
        }

        return false;
    }

    protected bool Equals(ILocalActionState other)
    {
        return other is ActualState;
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}