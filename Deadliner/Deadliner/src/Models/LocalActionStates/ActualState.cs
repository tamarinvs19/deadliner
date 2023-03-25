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
}