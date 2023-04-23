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

    public Group(int id, string title, string description, string? accessKey, IUser owner, ISuperGroup superGroup)
    {
        Id = id;
        Title = title;
        Description = description;
        AccessKey = accessKey;
        Owner = owner;
        SuperGroup = superGroup;
    }

    public override bool Equals(object? obj)
    {
        if (obj is IGroup other)
        {
            return Equals(other);
        }

        return false;
    }

    protected bool Equals(IGroup other)
    {
        return Id == other.Id && Title == other.Title && Description == other.Description &&
               AccessKey == other.AccessKey && Owner.Equals(other.Owner) && SuperGroup.Equals(other.SuperGroup);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Title, Description, AccessKey, Owner, SuperGroup);
    }


    public int Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? AccessKey { get; set; }
    public IUser Owner { get; set; }
    public ISuperGroup SuperGroup { get; set; }
}