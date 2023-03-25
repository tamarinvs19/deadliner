namespace Deadliner.Models;

public interface ISuperGroup : IObject
{
    string Title { get; set; }
    string Description { get; set; }
    string? AccessKey { get; set; }
    IUser Owner { get; set; }
}