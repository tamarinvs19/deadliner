namespace Deadliner.Api.Models;

public interface ILocalEvent : ILocalAction
{
    DateTime DateTime { get; set; }
}