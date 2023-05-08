namespace Deadliner.Storage.EF.ModelsDB;

public partial class Group
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? AccessKey { get; set; }

    public int Owner { get; set; }

    public int SuperGroup { get; set; }

    public virtual ICollection<LocalAction> LocalActions { get; set; } = new List<LocalAction>();

    public virtual User OwnerNavigation { get; set; } = null!;

    public virtual SuperGroup SuperGroupNavigation { get; set; } = null!;

    public virtual ICollection<UserToGroup> UserToGroups { get; set; } = new List<UserToGroup>();
}
