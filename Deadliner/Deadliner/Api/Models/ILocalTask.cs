namespace Deadliner.Models;

public interface ILocalTask : ILocalAction
{
    string Title { get; set; }
    string Description { get; set; }
    DateTime CreationDateTime { get; }
    DateTime Deadline { get; set; }
}