namespace Deadliner.Api.Models;

public interface ILocalTask : ILocalAction
{
    DateTime CreationDateTime { get; }
    DateTime Deadline { get; set; }
}