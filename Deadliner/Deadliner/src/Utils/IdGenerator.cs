namespace Deadliner.Utils;

public sealed class IdGenerator : IIdGenerator
{
    private static readonly Lazy<IIdGenerator> _instance = new Lazy<IIdGenerator>(() => new IdGenerator());
    private static int _nextId;

    public IdGenerator() { } 
    
    public static IIdGenerator Instance
    {
        get => _instance.Value;
    }
    
    public int NextId()
    {
        return _nextId++;
    }
}