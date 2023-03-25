using Deadliner.Utils;

namespace Deadliner.Models;

public class UserToLocalAction : IUserToLocalAction
{
    public UserToLocalAction(IUser user, ILocalAction localAction, ILocalActionState state)
    {
        Id = IdGenerator.Instance.NextId();
        User = user;
        LocalAction = localAction;
        State = state;
    }

    public int Id { get; }
    public IUser User { get; }
    public ILocalAction LocalAction { get; }
    public ILocalActionState State { get; set; }
}