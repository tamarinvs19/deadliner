namespace Deadliner.Api.Models;

public interface IUser : IObject
{
    string Username { get; }
    string Password { get; }
}