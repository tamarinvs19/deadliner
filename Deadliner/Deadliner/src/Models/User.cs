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

    public override bool Equals(object? obj)
    {
        if (obj is IUser user)
        {
            return Equals(user);
        }
        
        return false;
    }

    protected bool Equals(IUser other)
    {
        return Id == other.Id && Username == other.Username && Password == other.Password;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Username, Password);
    }

    public int Id { get; init; }
    public string Username { get; set; }
    public string Password { get; set; }
}