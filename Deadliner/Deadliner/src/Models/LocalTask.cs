using Deadliner.Utils;

namespace Deadliner.Models;

public class LocalTask : ILocalTask
{
    public LocalTask(string title, string description, DateTime creationDateTime, DateTime deadline, IGroup group, ILocalActionState state)
    {
        Id = IdGenerator.Instance.NextId();
        Title = title;
        Description = description;
        CreationDateTime = creationDateTime;
        Deadline = deadline;
        Group = group;
        State = state;
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