using System.Data.SqlClient;
using Deadliner.Models;
using Deadliner.Storage.Ado;
using Deadliner.Storage.Ado.DataProviders;
using Deadliner.Storage.Ado.Mappers;
using NUnit.Framework;

namespace DeadlinerTests.Ado;

[TestFixture]
public class SuperGroupDbTests
{
    [Test]
    public void TestConnection()
    {
        var provider = new SuperGroupDataProvider();
        var superGroups = provider.Items().ToList();
        Assert.That(superGroups.Count, Is.GreaterThan(0));
    }
    
    [Test]
    public void TestComplexOperations()
    {
        var provider = new SuperGroupDataProvider();
        var userProvider = new UserDataProvider();
        var user = userProvider.Get(1);
        provider.Create(new SuperGroup(2, "TestSuperGroup", "Random super group", "123", user));
        var superGroup = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(superGroup.Id, Is.EqualTo(2));
            Assert.That(superGroup.Title, Is.EqualTo("TestSuperGroup"));
        });

        superGroup.Title = "NewTestTitle";
        provider.Update(superGroup);
        
        var newSuperGroup = provider.Get(2);
        Assert.Multiple(() =>
        {
            Assert.That(newSuperGroup.Id, Is.EqualTo(2));
            Assert.That(newSuperGroup.Title, Is.EqualTo("NewTestTitle"));
        });
        
        provider.Delete(2);
        var deletedSuperGroup = provider.Get(2);
        Assert.IsNull(deletedSuperGroup);
    }
    
    [Test]
    public void TestNullAccessKey()
    {
        var provider = new SuperGroupDataProvider();
        var userProvider = new UserDataProvider();
        var user = userProvider.Get(1);
        provider.Create(new SuperGroup(3, "NullTestSuperGroup", "Random super group", null, user));
        var superGroup = provider.Get(3);
        Assert.Multiple(() =>
        {
            Assert.That(superGroup.Id, Is.EqualTo(3));
            Assert.IsNull(superGroup.AccessKey);
        });
        
        provider.Delete(3);
        var deletedSuperGroup = provider.Get(3);
        Assert.IsNull(deletedSuperGroup);
    }
}