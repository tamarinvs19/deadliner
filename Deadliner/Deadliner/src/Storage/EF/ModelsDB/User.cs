namespace Deadliner.Storage.EF.ModelsDB;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<SuperGroup> SuperGroups { get; set; } = new List<SuperGroup>();

    public virtual ICollection<UserToGroup> UserToGroups { get; set; } = new List<UserToGroup>();

    public virtual ICollection<UserToLocalAction> UserToLocalActions { get; set; } = new List<UserToLocalAction>();

    public virtual ICollection<UserToSuperGroup> UserToSuperGroups { get; set; } = new List<UserToSuperGroup>();
}
