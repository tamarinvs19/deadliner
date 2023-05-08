namespace Deadliner.Storage.EF.ModelsDB;

public partial class LocalEvent
{
    public int Id { get; set; }

    public DateTime DateTime { get; set; }

    public virtual LocalAction IdNavigation { get; set; } = null!;
}
