namespace Deadliner.Models;

public interface IUserToGroup : IObject
{
    IUser User { get; }
    IGroup Group { get; }
}