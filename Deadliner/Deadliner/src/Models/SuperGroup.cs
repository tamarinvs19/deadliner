using Deadliner.Utils;

namespace Deadliner.Models;

public class SuperGroup : ISuperGroup
{
    public SuperGroup(string title, string description, string? accessKey, IUser owner)
    {
        Id = IdGenerator.Instance.NextId();
        Title = title;
        Description = description;
        AccessKey = accessKey;
        Owner = owner;
    }

    public int Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? AccessKey { get; set; }
    public IUser Owner { get; set; }
}