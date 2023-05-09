using Deadliner.Storage.EF.DataProviders;
using Deadliner.Storage.EF.ModelsDB;
using NUnit.Framework;
using Group = Deadliner.Models.Group;

namespace DeadlinerTests.EF;

[TestFixture]
public class GroupDbTests
{
    [Test]
    public void TestConnection()
    {
        var context = new DeadlinerContext();
        var provider = new GroupDataProvider(context);
        var groups = provider.Items().ToList();
        Assert.That(groups, Is.Not.Empty);
    }
    
    [Test]
    public void TestComplexOperations()
    {
        var context = new DeadlinerContext();
        var provider = new GroupDataProvider(context);
        
        var userProvider = new UserDataProvider(context);
        var user = userProvider.Get(1);
        var superGroupProvider = new SuperGroupDataProvider(context);
        var superGroup = superGroupProvider.Get(1);
        
        provider.Create(new Group(2, "TestGroup", "Random group", "123", user, superGroup));
        provider.Save();
        var group = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(group.Id, Is.EqualTo(2));
            Assert.That(group.Title, Is.EqualTo("TestGroup"));
        });
    
        group.Title = "NewTestTitle";
        provider.Update(group);
        provider.Save();
        
        var newGroup = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(newGroup.Id, Is.EqualTo(2));
            Assert.That(newGroup.Title, Is.EqualTo("NewTestTitle"));
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
    //
    // [Test]
    // public void TestNullAccessKey()
    // {
    //     var provider = new SuperGroupDataProvider();
    //     var userProvider = new UserDataProvider();
    //     var user = userProvider.Get(1);
    //     provider.Create(new SuperGroup(3, "NullTestSuperGroup", "Random super group", null, user));
    //     var superGroup = provider.Get(3);
    //     Assert.Multiple(() =>
    //     {
    //         Assert.That(superGroup.Id, Is.EqualTo(3));
    //         Assert.IsNull(superGroup.AccessKey);
    //     });
    //     
    //     provider.Delete(3);
    //     var deletedSuperGroup = provider.Get(3);
    //     Assert.IsNull(deletedSuperGroup);
    // }
}