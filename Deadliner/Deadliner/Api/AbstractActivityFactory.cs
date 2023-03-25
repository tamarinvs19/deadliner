using Deadliner.Models;

namespace Deadliner;

public interface IAbstractActivityFactory  // Абстрактная фабрика
{
    public ILocalEvent MakeLocalEvent(string title, string description, DateTime datetime, IGroup group);
    public ILocalTask MakeTask(string title, string description, DateTime creationTime, DateTime deadline, IGroup group);
    public T AddChild<T>(T parentAction, T localAction) where T : ILocalAction;
}