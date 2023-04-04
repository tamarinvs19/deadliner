using Deadliner.Models;

namespace Deadliner.Api.Models;

public interface ISuperGroup : IObject
{
    string Title { get; set; }
    string Description { get; set; }
    string? AccessKey { get; set; }
    IUser Owner { get; set; }
}