using Deadliner;
using Deadliner.Api.Controller;
using Deadliner.Controller;
using NUnit.Framework;

namespace DeadlinerTests.DI;

[TestFixture]
public class DiTest
{
    [Test]
    public void TestTimeProvider()
    {
        MainContainer.BuildContainer();
        var timeProvider = MainContainer.TimeProvider();
        var today = DateTime.Today;
        Assert.That(today, Is.EqualTo(timeProvider.Now().Date));
    }
    
    [Test]
    public void TestIdGenerator()
    {
        MainContainer.BuildContainer();
        var idGenerator = MainContainer.IdGenerator();
        var firstId = idGenerator.NextId();
        var secondId = idGenerator.NextId();
        var thirdId = idGenerator.NextId();
        Assert.That(firstId, Is.Not.EqualTo(secondId));
        Assert.That(firstId, Is.Not.EqualTo(thirdId));
        Assert.That(secondId, Is.Not.EqualTo(thirdId));
    }

    private int IdGeneratorHelpFunction()
    {
        var idGenerator = MainContainer.IdGenerator();
        return idGenerator.NextId();
    }
    
    [Test]
    public void TestIdGeneratorFirst()
    {
        MainContainer.BuildContainer();
        var firstId = IdGeneratorHelpFunction();
        var secondId = IdGeneratorHelpFunction();
        Assert.That(firstId, Is.Not.EqualTo(secondId));
    }
    
    [Test]
    public void TestIdGeneratorSecond()
    {
        MainContainer.BuildContainer();
        var idGenerator = MainContainer.IdGenerator();
        var firstId = idGenerator.NextId();
        Assert.That(firstId >= 2);
    }
    
    [Test]
    public void TestContext()
    {
        MainContainer.BuildContainer();
        var context = MainContainer.Context();
        Assert.IsInstanceOf<IContext>(context);
    }
}