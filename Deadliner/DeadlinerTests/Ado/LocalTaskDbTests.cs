using System.Data.SqlClient;
using Deadliner.Models;
using Deadliner.Storage.Ado;
using Deadliner.Storage.Ado.DataProviders;
using Deadliner.Storage.Ado.Mappers;
using Deadliner.Utils;
using NUnit.Framework;

namespace DeadlinerTests.Ado;

[TestFixture]
public class LocalTaskDbTests
{
    [Test]
    public void TestConnection()
    {
        var provider = new LocalTaskDataProvider();
        var tasks = provider.Items().ToList();
        Assert.That(tasks, Is.Not.Empty);
    }
    
    [Test]
    public void TestComplexOperations()
    {
        var provider = new LocalTaskDataProvider();

        var groupProvider = new GroupDataProvider();
        var group = groupProvider.Get(1);

        provider.Create( 
            new LocalTask(
                2,
                "TestTask", 
                "Random task",
                new DateTime(2023, 04, 01),
            new DateTime(2023, 05, 01),
                group,
                StateIdTransformer.GetState(0), null
                )
            );
        var localTask = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(localTask.Id, Is.EqualTo(2));
            Assert.That(localTask.Title, Is.EqualTo("TestTask"));
        });
    
        localTask.Title = "NewTaskTitle";
        provider.Update(localTask);
        
        var newTask = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(newTask.Id, Is.EqualTo(2));
            Assert.That(newTask.Title, Is.EqualTo("NewTaskTitle"));
        });
        
        provider.Delete(2);
        var deletedTask = provider.Get(2);
        Assert.IsNull(deletedTask);
    }
    
    [Test]
    public void TestNullAccessKey()
    {
        var provider = new LocalTaskDataProvider();

        var groupProvider = new GroupDataProvider();
        var group = groupProvider.Get(1);

        var oldTask = provider.Get(1);

        provider.Create( 
            new LocalTask(
                2,
                "TestTask", 
                "Random task",
                new DateTime(2023, 04, 01),
            new DateTime(2023, 05, 01),
                group,
                StateIdTransformer.GetState(0),
                oldTask
                )
            );
        var localTask = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(localTask.Id, Is.EqualTo(2));
            Assert.That(localTask.Parent, Is.Not.Null);
            Assert.That(localTask.Parent?.Id, Is.EqualTo(oldTask.Id));
        });
        
        provider.Delete(2);
        var deletedTask = provider.Get(2);
        Assert.IsNull(deletedTask);
    }
}