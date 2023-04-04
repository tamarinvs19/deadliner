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

    public int Id { get; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }
    public ILocalAction? Parent { get; set; }
    
    public IGroup Group { get; set; }
    public ILocalActionState State { get; set; }
}