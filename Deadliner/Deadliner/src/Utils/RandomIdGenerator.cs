using Deadliner.Api.Utils;

namespace Deadliner.Utils;

public sealed class RandomIdGenerator : IIdGenerator
{
    private static readonly Lazy<IIdGenerator> _instance = new Lazy<IIdGenerator>(() => new IdGenerator());
    private static int _nextId;

    public RandomIdGenerator() { } 
    
    public static IIdGenerator Instance
    {
        get => _instance.Value;
    }
    
    public int NextId()
    {
        _nextId = new Random().Next();
        return _nextId;
    }
}