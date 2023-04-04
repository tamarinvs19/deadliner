using Deadliner.Api.Utils;
using Deadliner.Utils;

namespace Deadliner.Api.Models.LocalActionStates;

public interface ILocalActionState
{
     ListOfTypes<ILocalActionState> Neighbours { get; }
     Func<ILocalActionState?> Handle();
}