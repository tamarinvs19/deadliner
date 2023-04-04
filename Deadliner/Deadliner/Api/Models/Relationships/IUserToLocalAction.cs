using Deadliner.Api.Models.LocalActionStates;
using Deadliner.Models;

namespace Deadliner.Api.Models.Relationships;

public interface IUserToLocalAction : IObject
{
    IUser User { get; }
    ILocalAction LocalAction { get; }
    ILocalActionState State { get; set; }
}