using Deadliner.Utils;

namespace Deadliner.Models.LocalActionStates;

public class OverdueState : ILocalActionState
{
    private ListOfTypes<ILocalActionState> _neigbours = new();
    public ListOfTypes<ILocalActionState> Neighbours
    {
        get => _neigbours;
    }

    public Func<ILocalActionState?> Handle()
    {
        throw new NotImplementedException();
    }
}