using Deadliner.Api.Models;
using Deadliner.Models;

namespace Deadliner.Utils;

public static class LocalActionTypeTransformer
{
    public static Type GetType(int id)
    {
        return id switch
        {
            0 => typeof(LocalEvent),
            1 => typeof(LocalTask),
            _ => throw new ArgumentOutOfRangeException(nameof(id), id, $"Invalid type id {id}")
        };
    }
    
    public static int GetTypeId(ILocalAction action)
    {
        return action switch
        {
            ILocalEvent => 0,
            ILocalTask => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(action), action, $"Invalid action type {action}")
        };
    }
}