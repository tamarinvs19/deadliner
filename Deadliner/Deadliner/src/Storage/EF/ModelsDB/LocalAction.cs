namespace Deadliner.Storage.EF.ModelsDB;

public partial class LocalAction
{
    public int Id { get; set; }

    public int State { get; set; }

    public int? Parent { get; set; }

    public int Dgroup { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Type { get; set; }

    public virtual Group DgroupNavigation { get; set; } = null!;

    public virtual ICollection<LocalAction> InverseParentNavigation { get; set; } = new List<LocalAction>();

    public virtual LocalEvent? LocalEvent { get; set; }

    public virtual LocalTask? LocalTask { get; set; }

    public virtual LocalAction? ParentNavigation { get; set; }

    public virtual ICollection<UserToLocalAction> UserToLocalActions { get; set; } = new List<UserToLocalAction>();
}
