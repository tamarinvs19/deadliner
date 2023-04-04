using Deadliner.Models;

namespace Deadliner.Api.Models.Relationships;

public interface IUserToGroup : IObject
{
    IUser User { get; }
    IGroup Group { get; }
}