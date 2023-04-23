using Deadliner.Api.Models;
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

    public SuperGroup(int id, string title, string description, string? accessKey, IUser owner)
    {
        Id = id;
        Title = title;
        Description = description;
        AccessKey = accessKey;
        Owner = owner;
    }

    public override bool Equals(object? obj)
    {
        if (obj is ISuperGroup other)
        {
            return Equals(other);
        }

        return false;
    }

    protected bool Equals(ISuperGroup other)
    {
        return Id == other.Id && Title == other.Title && Description == other.Description &&
               AccessKey == other.AccessKey && Owner.Equals(other.Owner);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Title, Description, AccessKey, Owner);
    }

    public int Id { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? AccessKey { get; set; }
    public IUser Owner { get; set; }
}