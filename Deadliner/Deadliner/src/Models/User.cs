using Deadliner.Api.Models;
using Deadliner.Utils;

namespace Deadliner.Models;

public class User : IUser
{
    public IUser CreateUser(string username, string password)
    {
        return new User
        {
            Id = IdGenerator.Instance.NextId(),
            Username = username,
            Password = password
        };
    }

    public int Id { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}