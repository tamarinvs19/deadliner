namespace Deadliner.Models;

public interface ILocalEvent : ILocalAction
{
    string Title { get; set; }
    string Description { get; set; }
    DateTime DateTime { get; set; }
}