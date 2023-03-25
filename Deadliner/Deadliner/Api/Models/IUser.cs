namespace Deadliner.Models;

public interface IUser : IObject
{
    string Username { get; }
    string Password { get; }
}