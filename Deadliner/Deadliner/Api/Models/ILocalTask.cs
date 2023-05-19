namespace Deadliner.Api.Models;

public interface ILocalTask : ILocalAction
{
    DateTime CreationDateTime { get; set; }
    DateTime Deadline { get; set; }
}