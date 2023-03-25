namespace Deadliner.Models;

public interface ILocalAction : IObject  // Компоновщик (ITask + ILocalEvent)
{
    IGroup Group { get; set; }
    ILocalActionState State { get; set; }  // Состояние
    ILocalAction? Parent { get; set; }
}