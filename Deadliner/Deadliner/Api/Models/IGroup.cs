namespace Deadliner.Models;

public interface IGroup : IObject
{
    string Title { get; set; }
    string Description { get; set; }
    string? AccessKey { get; set; }
    IUser Owner { get; set; }
    ISuperGroup SuperGroup { get; set; }
}