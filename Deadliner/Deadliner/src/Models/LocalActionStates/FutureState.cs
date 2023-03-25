using Deadliner.Utils;

namespace Deadliner.Models.LocalActionStates;

public class FutureState : ILocalActionState
{
    private ListOfTypes<ILocalActionState> _neigbours = new(){ typeof(ActualState) };
    public ListOfTypes<ILocalActionState> Neighbours
    {
        get => _neigbours;
    }
    public Func<ILocalActionState?> Handle()
    {
        throw new NotImplementedException();
    }
}