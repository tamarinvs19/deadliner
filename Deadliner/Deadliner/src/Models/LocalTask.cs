using Deadliner.Api.Models;
using Deadliner.Api.Models.LocalActionStates;
using Deadliner.Utils;

namespace Deadliner.Models;

public class LocalTask : ILocalTask
{
    public LocalTask(int id, string title, string description, DateTime creationDateTime, DateTime deadline,
        IGroup group, ILocalActionState state, ILocalAction? parent)
    {
        Id = id;
        Title = title;
        Description = description;
        CreationDateTime = creationDateTime;
        Deadline = deadline;
        Group = group;
        State = state;
        Parent = parent;
    }

    public LocalTask(string title, string description, DateTime creationDateTime, DateTime deadline, IGroup group,
        ILocalActionState state)
    {
        Id = IdGenerator.Instance.NextId();
        Title = title;
        Description = description;
        CreationDateTime = creationDateTime;
        Deadline = deadline;
        Group = group;
        State = state;
    }

    public override bool Equals(object? obj)
    {
        if (obj is ILocalTask other)
        {
            return Equals(other);
        }

        return false;
    }

    protected bool Equals(ILocalTask other)
    {
        return Id == other.Id && Title == other.Title && Description == other.Description &&
               CreationDateTime.Equals(other.CreationDateTime) && Deadline.Equals(other.Deadline) &&
               Equals(Parent, other.Parent) && Group.Equals(other.Group) && State.Equals(other.State);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Title, Description, CreationDateTime, Deadline, Parent, Group, State);
    }

    public int Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreationDateTime { get; }
    public DateTime Deadline { get; set; }
    public ILocalAction? Parent { get; set; }

    public IGroup Group { get; set; }
    public ILocalActionState State { get; set; }
}