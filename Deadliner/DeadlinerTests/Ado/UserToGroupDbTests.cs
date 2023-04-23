using System.Data.SqlClient;
using Deadliner.Models;
using Deadliner.Storage.Ado;
using Deadliner.Storage.Ado.DataProviders;
using Deadliner.Storage.Ado.Mappers;
using NUnit.Framework;

namespace DeadlinerTests.Ado;

[TestFixture]
public class UserToGroupDbTests
{
    [Test]
    public void TestComplexOperations()
    {
        var provider = new UserToGroupDataProvider();

        var userProvider = new UserDataProvider();
        var groupProvider = new GroupDataProvider();

        userProvider.Create(new User { Id=2, Username="a", Password="b" });
        var user = userProvider.Get(2);
        var group = groupProvider.Get(1);
        
        provider.Create(new UserToGroup(2, user, group));
        var relation = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(relation.Id, Is.EqualTo(2));
            Assert.That(relation.User, Is.EqualTo(user));
            Assert.That(relation.Group, Is.EqualTo(group));
        });

        provider.Delete(2);
        var deletedRelation = provider.Get(2);
        Assert.IsNull(deletedRelation);
        
        userProvider.Delete(2);
    }
}