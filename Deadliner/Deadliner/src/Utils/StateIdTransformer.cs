using Deadliner.Api.Models.LocalActionStates;
using Deadliner.Models.LocalActionStates;

namespace Deadliner.Utils;

public static class StateIdTransformer
{
    public static ILocalActionState GetState(int id)
    {
        return id switch
        {
            0 => new OverdueState(),
            1 => new ActualState(),
            2 => new FutureState(),
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, $"Invalid state id {id}")
        };
    }
    
    public static int GetStateId(ILocalActionState state)
    {
        return state switch
        {
            OverdueState => 0,
            ActualState => 1,
            FutureState => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, $"Invalid state {state}")
        };
    }
}