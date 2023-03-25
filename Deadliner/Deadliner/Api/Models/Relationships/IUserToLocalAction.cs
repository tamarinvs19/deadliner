namespace Deadliner.Models;

public interface IUserToLocalAction : IObject
{
    IUser User { get; }
    ILocalAction LocalAction { get; }
    ILocalActionState State { get; set; }
}