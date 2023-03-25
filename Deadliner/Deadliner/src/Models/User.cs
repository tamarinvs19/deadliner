using Deadliner.Utils;

namespace Deadliner.Models;

public class User : IUser
{
    public User(string username, string password)
    {
        Id = IdGenerator.Instance.NextId();
        Username = username;
        Password = password;
    }

    public int Id { get; }
    public string Username { get; }
    public string Password { get; }
}