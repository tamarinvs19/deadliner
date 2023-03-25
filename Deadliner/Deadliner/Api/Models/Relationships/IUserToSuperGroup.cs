namespace Deadliner.Models;

public interface IUserToSuperGroup : IObject
{
    IUser User { get; }
    ISuperGroup SuperGroup { get; }
}