using System.Data.SqlClient;
using Deadliner.Models;
using Deadliner.Storage.Ado;
using Deadliner.Storage.Ado.DataProviders;
using Deadliner.Storage.Ado.Mappers;
using NUnit.Framework;

namespace DeadlinerTests.Ado;

[TestFixture]
public class UserDbTests
{
    [Test]
    public void TestConnection()
    {
        var connectionString = "Data Source=THINKBOOK;Initial Catalog=DEADLINER;Integrated Security=True";
        var provider = new UserDataProvider(connectionString);
        var users = provider.Items().ToList();
        Assert.That(users.Count, Is.GreaterThan(0));
    }
    
    [Test]
    public void TestComplexOperations()
    {
        var connectionString = "Data Source=THINKBOOK;Initial Catalog=DEADLINER;Integrated Security=True";
        var provider = new UserDataProvider(connectionString);
        provider.Create(new User {Id = 2, Username = "TestUser", Password = "TestPassword"} );
        var user = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(user.Id, Is.EqualTo(2));
            Assert.That(user.Username, Is.EqualTo("TestUser"));
        });

        user.Username = "NewTestUser";
        provider.Update(user);
        
        var newUser = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(newUser.Id, Is.EqualTo(2));
            Assert.That(newUser.Username, Is.EqualTo("NewTestUser"));
        });
        
        provider.Delete(2);
        var deletedUser = provider.Get(2);
        Assert.IsNull(deletedUser);
    }
}