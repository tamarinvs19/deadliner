using Deadliner.Api.Utils;

namespace Deadliner.Utils;

public class TimeProvider : ITimeProvider
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}