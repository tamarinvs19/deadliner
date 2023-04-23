using System.Data.SqlClient;
using Deadliner.Models;
using Deadliner.Models.LocalActionStates;
using Deadliner.Storage.Ado;
using Deadliner.Storage.Ado.DataProviders;
using Deadliner.Storage.Ado.Mappers;
using NUnit.Framework;

namespace DeadlinerTests.Ado;

[TestFixture]
public class UserToLocalActionDbTests
{
    [Test]
    public void TestComplexOperations()
    {
        var provider = new UserToLocalActionDataProvider();

        var userProvider = new UserDataProvider();
        var taskProvider = new LocalTaskDataProvider();
        
        var user = userProvider.Get(1);
        var task = taskProvider.Get(1);
        var state = new ActualState();
        
        provider.Create(new UserToLocalAction(1, user, task, state));
        var relation = provider.Get(1);
        Assert.Multiple(() =>
        {
            Assert.That(relation.Id, Is.EqualTo(1));
            Assert.That(relation.User, Is.EqualTo(user));
            Assert.That(relation.LocalAction.Id, Is.EqualTo(task.Id));
            Assert.That(relation.State, Is.EqualTo(state));
        });

        provider.Delete(1);
        var deletedRelation = provider.Get(1);
        Assert.IsNull(deletedRelation);
    }
}