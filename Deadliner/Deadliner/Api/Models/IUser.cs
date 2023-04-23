namespace Deadliner.Api.Models;

public interface IUser : IObject
{
    string Username { get; set; }
    string Password { get; set; }
}