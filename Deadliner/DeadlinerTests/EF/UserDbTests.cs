using Deadliner.Storage.EF.DataProviders;
using Deadliner.Storage.EF.ModelsDB;
using NUnit.Framework;
using User = Deadliner.Models.User;

namespace DeadlinerTests.EF;

[TestFixture]
public class UserDbTests
{
    [Test]
    public void TestConnection()
    {
        var context = new DeadlinerContext();
        var users = context.Users.ToList();
        Assert.That(users.Count, Is.GreaterThan(0));
    }
    
    [Test]
    public void TestComplexOperations()
    {
        var context = new DeadlinerContext();
        var provider = new UserDataProvider(context);
        provider.Create(new User {Id = 2, Username = "TestUser", Password = "TestPassword"} );
        provider.Save();
        var user = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(user.Id, Is.EqualTo(2));
            Assert.That(user.Username, Is.EqualTo("TestUser"));
        });
        
        user.Username = "NewTestUser";
        provider.Update(user);
        provider.Save();
        
        var newUser = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(newUser.Id, Is.EqualTo(2));
            Assert.That(newUser.Username, Is.EqualTo("NewTestUser"));
        });
        
        provider.Delete(2);
        provider.Save();
        try
        {
            var item = provider.Get(2);
            Assert.That(item, Is.Null);
        }
        catch (InvalidOperationException e)
        {
            Assert.True(true);
        }
    }
}