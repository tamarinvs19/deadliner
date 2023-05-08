namespace Deadliner.Storage.EF.ModelsDB;

public partial class LocalTask
{
    public int Id { get; set; }

    public DateTime CreationDateTime { get; set; }

    public DateTime Deadline { get; set; }

    public virtual LocalAction IdNavigation { get; set; } = null!;
}
