using Deadliner.Api.Models;
using Deadliner.Api.Models.LocalActionStates;
using Deadliner.Utils;

namespace Deadliner.Models;

public class LocalEvent : ILocalEvent
{
    public LocalEvent(string title, string description, DateTime dateTime, IGroup group, ILocalActionState state)
    {
        Id = IdGenerator.Instance.NextId();
        Title = title;
        Description = description;
        DateTime = dateTime;
        Group = group;
        State = state;
    }

    public LocalEvent(int id, string title, string description, DateTime dateTime, IGroup group, ILocalActionState state, ILocalAction? parent)
    {
        Id = id;
        Title = title;
        Description = description;
        DateTime = dateTime;
        Group = group;
        State = state;
        Parent = parent;
    }

    public override bool Equals(object? obj)
    {
        if (obj is ILocalEvent other)
        {
            return Equals(other);
        }

        return false;
    }

    protected bool Equals(ILocalEvent other)
    {
        return Id == other.Id && Title == other.Title && Description == other.Description &&
               DateTime.Equals(other.DateTime) && Equals(Parent, other.Parent) && Group.Equals(other.Group) &&
               State.Equals(other.State);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Title, Description, DateTime, Parent, Group, State);
    }

    public int Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }
    public ILocalAction? Parent { get; set; }
    
    public IGroup Group { get; set; }
    public ILocalActionState State { get; set; }
}