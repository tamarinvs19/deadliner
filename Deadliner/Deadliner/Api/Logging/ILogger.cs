using Microsoft.Extensions.Logging;

namespace Deadliner.Api.Logging;

public interface IBaseLogger<out T>
{
    ILogger<T> Logger { get; }
}