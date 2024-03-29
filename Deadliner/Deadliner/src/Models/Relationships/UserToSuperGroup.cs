using Deadliner.Api.Models;
using Deadliner.Api.Models.Relationships;
using Deadliner.Utils;

namespace Deadliner.Models;

public class UserToSuperGroup : IUserToSuperGroup
{
    public UserToSuperGroup(IUser user, ISuperGroup superGroup)
    {
        Id = IdGenerator.Instance.NextId();
        User = user;
        SuperGroup = superGroup;
    }

    public UserToSuperGroup(int id, IUser user, ISuperGroup superGroup)
    {
        Id = id;
        User = user;
        SuperGroup = superGroup;
    }

    public int Id { get; }
    public IUser User { get; }
    public ISuperGroup SuperGroup { get; }
}