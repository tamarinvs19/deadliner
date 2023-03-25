using Deadliner.Utils;

namespace Deadliner.Models;

public interface ILocalActionState
{
     ListOfTypes<ILocalActionState> Neighbours { get; }
     Func<ILocalActionState?> Handle();
}