namespace Deadliner.Storage.EF.ModelsDB;

public partial class UserToLocalAction
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int LocalActionId { get; set; }

    public int StateId { get; set; }

    public virtual LocalAction LocalAction { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
