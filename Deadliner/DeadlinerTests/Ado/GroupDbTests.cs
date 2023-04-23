using System.Data.SqlClient;
using Deadliner.Models;
using Deadliner.Storage.Ado;
using Deadliner.Storage.Ado.DataProviders;
using Deadliner.Storage.Ado.Mappers;
using NUnit.Framework;

namespace DeadlinerTests.Ado;

[TestFixture]
public class GroupDbTests
{
    [Test]
    public void TestConnection()
    {
        var provider = new GroupDataProvider();
        var groups = provider.Items().ToList();
        Assert.That(groups.Count, Is.GreaterThan(0));
    }
    
    [Test]
    public void TestComplexOperations()
    {
        var provider = new GroupDataProvider();
        
        var userProvider = new UserDataProvider();
        var user = userProvider.Get(1);
        var superGroupProvider = new SuperGroupDataProvider();
        var superGroup = superGroupProvider.Get(1);
        
        provider.Create(new Group(2, "TestGroup", "Random group", "123", user, superGroup));
        var group = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(group.Id, Is.EqualTo(2));
            Assert.That(group.Title, Is.EqualTo("TestGroup"));
        });
    
        group.Title = "NewTestTitle";
        provider.Update(group);
        
        var newGroup = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(newGroup.Id, Is.EqualTo(2));
            Assert.That(newGroup.Title, Is.EqualTo("NewTestTitle"));
        });
        
        provider.Delete(2);
        var deletedGroup = provider.Get(2);
        Assert.IsNull(deletedGroup);
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