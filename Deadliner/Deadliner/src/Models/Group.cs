using Deadliner.Api.Models;
using Deadliner.Utils;

namespace Deadliner.Models;

public class Group : IGroup
{
    public Group(string title, string description, string? accessKey, IUser owner, ISuperGroup superGroup)
    {
        Id = IdGenerator.Instance.NextId();
        Title = title;
        Description = description;
        AccessKey = accessKey;
        Owner = owner;
        SuperGroup = superGroup;
    }

    public int Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? AccessKey { get; set; }
    public IUser Owner { get; set; }
    public ISuperGroup SuperGroup { get; set; }
}