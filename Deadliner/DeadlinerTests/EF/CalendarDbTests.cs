using Deadliner.Api.Models;
using Deadliner.Controller;
using Deadliner.Models;
using NUnit.Framework;

namespace DeadlinerTests.EF;

[TestFixture]
public class CalendarDbTests
{
    [Test]
    public void TestConnection()
    {
        var dbContext = new DbContext();
        var user = dbContext.Users.Get(1);
        dbContext.Calendars.Create(new Calendar(user, new List<ILocalTask>()));
        dbContext.Calendars.Save();
        var calendars = dbContext.Calendars.Items();
        Assert.That(calendars.ToList().Count, Is.GreaterThan(0));
    }
}