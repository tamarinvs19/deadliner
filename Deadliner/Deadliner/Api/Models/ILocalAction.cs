using Deadliner.Api.Models.LocalActionStates;
using Deadliner.Models;

namespace Deadliner.Api.Models;

public interface ILocalAction : IObject  // Компоновщик (ILocalTask + ILocalEvent)
{
    string Title { get; set; }
    string Description { get; set; }
    IGroup Group { get; set; }
    ILocalActionState State { get; set; }  // Состояние
    ILocalAction? Parent { get; set; }
}