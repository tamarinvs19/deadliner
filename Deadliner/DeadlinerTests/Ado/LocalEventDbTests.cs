using Deadliner.Models;
using Deadliner.Storage.Ado.DataProviders;
using Deadliner.Utils;
using NUnit.Framework;

namespace DeadlinerTests.Ado;

[TestFixture]
public class LocalEventDbTests
{
    [Test]
    public void TestConnection()
    {
        var provider = new LocalEventDataProvider();
        var localEvents = provider.Items().ToList();
        Assert.That(localEvents, Is.Not.Empty);
    }
    
    [Test]
    public void TestComplexOperations()
    {
        var provider = new LocalEventDataProvider();

        var groupProvider = new GroupDataProvider();
        var group = groupProvider.Get(1);

        provider.Create( 
            new LocalEvent(
                2,
                "TestEvent", 
                "Random task",
                new DateTime(2023, 05, 01),
                group,
                StateIdTransformer.GetState(0), null
                )
            );
        var localEvent = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(localEvent.Id, Is.EqualTo(2));
            Assert.That(localEvent.Title, Is.EqualTo("TestEvent"));
        });
    
        localEvent.Title = "NewEvent";
        provider.Update(localEvent);
        
        var newTask = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(newTask.Id, Is.EqualTo(2));
            Assert.That(newTask.Title, Is.EqualTo("NewEvent"));
        });
        
        provider.Delete(2);
        var deletedEvent = provider.Get(2);
        Assert.IsNull(deletedEvent);
    }
    
    [Test]
    public void TestNullAccessKey()
    {
        var provider = new LocalEventDataProvider();
        var taskProvider = new LocalTaskDataProvider();

        var groupProvider = new GroupDataProvider();
        var group = groupProvider.Get(1);
        
        var oldTask = taskProvider.Get(1);
        
        provider.Create( 
            new LocalEvent(
                2,
                "TestEvent", 
                "Random task",
                new DateTime(2023, 05, 01),
                group,
                StateIdTransformer.GetState(0),
                oldTask
                )
            );
        var localEvent = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(localEvent.Id, Is.EqualTo(2));
            Assert.That(localEvent.Parent, Is.Not.Null);
            Assert.That(localEvent.Parent?.Id, Is.EqualTo(oldTask.Id));
        });
        
        provider.Delete(2);
        var deletedTask = provider.Get(2);
        Assert.IsNull(deletedTask);
    }
}