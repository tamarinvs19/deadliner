namespace Deadliner.Storage.EF.ModelsDB;

public partial class UserToSuperGroup
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int SuperGroupId { get; set; }

    public virtual SuperGroup SuperGroup { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
