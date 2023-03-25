using Deadliner.Utils;

namespace Deadliner.Models;

public class UserToGroup : IUserToGroup
{
    public UserToGroup(IUser user, IGroup group)
    {
        Id = IdGenerator.Instance.NextId();
        User = user;
        Group = group;
    }

    public int Id { get; }
    public IUser User { get; }
    public IGroup Group { get; }
}