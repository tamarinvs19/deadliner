namespace Deadliner.Storage.EF.ModelsDB;

public partial class SuperGroup
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string? AccessKey { get; set; }

    public int Owner { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual User OwnerNavigation { get; set; } = null!;

    public virtual ICollection<UserToSuperGroup> UserToSuperGroups { get; set; } = new List<UserToSuperGroup>();
}
