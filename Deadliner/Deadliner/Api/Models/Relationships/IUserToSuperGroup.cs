using Deadliner.Models;

namespace Deadliner.Api.Models.Relationships;

public interface IUserToSuperGroup : IObject
{
    IUser User { get; }
    ISuperGroup SuperGroup { get; }
}