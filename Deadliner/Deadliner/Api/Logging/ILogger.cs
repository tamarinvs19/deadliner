namespace Deadliner.Logging;

using Microsoft.Extensions.Logging;

public interface IBaseLogger<out T>
{
    ILogger<T> Logger { get; }
}