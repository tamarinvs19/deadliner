using Deadliner;
using Deadliner.Controller;
using Deadliner.Models;
using Deadliner.Models.LocalActionStates;
using Deadliner.Utils;
using NUnit.Framework;

namespace DeadlinerTests.Factories;

[TestFixture]
public class ActivityFactoryTest
{
    internal class SuperGroupStub : ISuperGroup
    {
        public SuperGroupStub(int id, string title, string description, string? accessKey, IUser owner)
        {
            Id = id;
            Title = title;
            Description = description;
            AccessKey = accessKey;
            Owner = owner;
        }

        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? AccessKey { get; set; }
        public IUser Owner { get; set; }
    }
    internal class UserStub : IUser
    {
        public UserStub(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        public int Id { get; }
        public string Username { get; }
        public string Password { get; }
    }
    internal class GroupStub : IGroup
    {
        public GroupStub(int id, string title, string description, string? accessKey, IUser owner, ISuperGroup superGroup)
        {
            Id = id;
            Title = title;
            Description = description;
            AccessKey = accessKey;
            Owner = owner;
            SuperGroup = superGroup;
        }

        public int Id { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? AccessKey { get; set; }
        public IUser Owner { get; set; }
        public ISuperGroup SuperGroup { get; set; }
    }
    
    internal class TimeProviderMock : ITimeProvider
    {
        private DateTime _current;
        
        public DateTime Now()
        {
            return _current;
        }

        public void SetTime(DateTime current)
        {
            _current = current;
        }
    }
    
    internal class MockContext : IContext
    {
        private TimeProviderMock _timeProvider;
        public MockContext()
        {
            SuperGroups = new GenericController<ISuperGroup>();
            Groups = new GenericController<IGroup>();
            Users = new GenericController<IUser>();
            LocalTasks = new GenericController<ILocalTask>();
            LocalEvents = new GenericController<ILocalEvent>();
            UserToSuperGroup = new GenericController<IUserToSuperGroup>();
            UserToGroup = new GenericController<IUserToGroup>();
            UserToLocalAction = new GenericController<IUserToLocalAction>();
            _timeProvider = new TimeProviderMock();
        }
        public DateTime CurrentDateTime
        {
            get => _timeProvider.Now();
            set => _timeProvider.SetTime(value);
        }
        public IController<ISuperGroup> SuperGroups { get; }
        public IController<IGroup> Groups { get; }
        public IController<IUser> Users { get; }
        public IController<ILocalTask> LocalTasks { get; }
        public IController<ILocalEvent> LocalEvents { get; }
        public IController<IUserToSuperGroup> UserToSuperGroup { get; }
        public IController<IUserToGroup> UserToGroup { get; }
        public IController<IUserToLocalAction> UserToLocalAction { get; }
        public ITimeProvider TimeProvider
        {
            get => _timeProvider;
        }
    }

    [Test]
    public void TestMakeLocalEventOverdue()
    {
        var context = new MockContext
        {
            CurrentDateTime = new DateTime(2023, 3, 1)
        };
        var activityFactory = new ActivityFactory(context);
        var owner = new UserStub(1, "A", "B");
        var superGroup = new SuperGroupStub(2, "Super", "Super...", null, owner);
        var group = new GroupStub(3, "Group", "Group...", null, owner, superGroup);
        var datetime = new DateTime(2023, 2, 23);
        var actual = activityFactory.MakeLocalEvent("Test", "Test...", datetime, group);
        
        Assert.That(actual.Title == "Test");
        Assert.That(actual.Description == "Test...");
        Assert.That(actual.DateTime == datetime);
        Assert.That(actual.Group == group);
        Assert.That(actual.State is OverdueState);
    }
    
    [Test]
    public void TestMakeLocalEventFuture()
    {
        var context = new MockContext
        {
            CurrentDateTime = new DateTime(2023, 3, 1)
        };
        var activityFactory = new ActivityFactory(context);
        var owner = new UserStub(1, "A", "B");
        var superGroup = new SuperGroupStub(2, "Super", "Super...", null, owner);
        var group = new GroupStub(3, "Group", "Group...", null, owner, superGroup);
        var datetime = new DateTime(2023, 3, 23);
        var actual = activityFactory.MakeLocalEvent("Test", "Test...", datetime, group);
        
        Assert.That(actual.Title == "Test");
        Assert.That(actual.Description == "Test...");
        Assert.That(actual.DateTime == datetime);
        Assert.That(actual.Group == group);
        Assert.That(actual.State is FutureState);
    }
    
    [Test]
    public void TestMakeLocalTaskFuture()
    {
        var context = new MockContext
        {
            CurrentDateTime = new DateTime(2023, 3, 1)
        };
        var activityFactory = new ActivityFactory(context);
        var owner = new UserStub(1, "A", "B");
        var superGroup = new SuperGroupStub(2, "Super", "Super...", null, owner);
        var group = new GroupStub(3, "Group", "Group...", null, owner, superGroup);
        var creationDatetime = new DateTime(2023, 3, 10);
        var deadline = new DateTime(2023, 3, 28);
        var actual = activityFactory.MakeTask("Test", "Test...", creationDatetime, deadline, group);
        
        Assert.That(actual.Title == "Test");
        Assert.That(actual.Description == "Test...");
        Assert.That(actual.CreationDateTime == creationDatetime);
        Assert.That(actual.Deadline == deadline);
        Assert.That(actual.Group == group);
        Assert.That(actual.State is FutureState);
    }
    
    [Test]
    public void TestMakeLocalTaskActual()
    {
        var context = new MockContext
        {
            CurrentDateTime = new DateTime(2023, 3, 1)
        };
        var activityFactory = new ActivityFactory(context);
        var owner = new UserStub(1, "A", "B");
        var superGroup = new SuperGroupStub(2, "Super", "Super...", null, owner);
        var group = new GroupStub(3, "Group", "Group...", null, owner, superGroup);
        var creationDatetime = new DateTime(2023, 2, 1);
        var deadline = new DateTime(2023, 3, 28);
        var actual = activityFactory.MakeTask("Test", "Test...", creationDatetime, deadline, group);
        
        Assert.That(actual.Title == "Test");
        Assert.That(actual.Description == "Test...");
        Assert.That(actual.CreationDateTime == creationDatetime);
        Assert.That(actual.Deadline == deadline);
        Assert.That(actual.Group == group);
        Assert.That(actual.State is ActualState);
    }
    
    [Test]
    public void TestMakeLocalTaskOverdue()
    {
        var context = new MockContext
        {
            CurrentDateTime = new DateTime(2023, 3, 1)
        };
        var activityFactory = new ActivityFactory(context);
        var owner = new UserStub(1, "A", "B");
        var superGroup = new SuperGroupStub(2, "Super", "Super...", null, owner);
        var group = new GroupStub(3, "Group", "Group...", null, owner, superGroup);
        var creationDatetime = new DateTime(2023, 2, 1);
        var deadline = new DateTime(2023, 2, 28);
        var actual = activityFactory.MakeTask("Test", "Test...", creationDatetime, deadline, group);
        
        Assert.That(actual.Title == "Test");
        Assert.That(actual.Description == "Test...");
        Assert.That(actual.CreationDateTime == creationDatetime);
        Assert.That(actual.Deadline == deadline);
        Assert.That(actual.Group == group);
        Assert.That(actual.State is OverdueState);
    }
    
    [Test]
    public void TestMakeLocalTaskInvalidTime()
    {
        var context = new BaseContext();
        var activityFactory = new ActivityFactory(context);
        var owner = new UserStub(1, "A", "B");
        var superGroup = new SuperGroupStub(2, "Super", "Super...", null, owner);
        var group = new GroupStub(3, "Group", "Group...", null, owner, superGroup);
        var creationDatetime = new DateTime(2023, 3, 29);
        var deadline = new DateTime(2023, 2, 28);
        Assert.Catch<ArgumentException>(() =>
            activityFactory.MakeTask("Test", "Test...", creationDatetime, deadline, group)
        );
    }

    [Test]
    public void TestAddChild()
    {
        var context = new MockContext
        {
            CurrentDateTime = new DateTime(2023, 3, 1)
        };
        var activityFactory = new ActivityFactory(context);
        var owner = new UserStub(1, "A", "B");
        var superGroup = new SuperGroupStub(2, "Super", "Super...", null, owner);
        var group = new GroupStub(3, "Group", "Group...", null, owner, superGroup);
        var creationDatetime = new DateTime(2023, 2, 1);
        var deadline = new DateTime(2023, 2, 28);
        var task = activityFactory.MakeTask("Test", "Test...", creationDatetime, deadline, group);
        var child = activityFactory.MakeTask("Child", "Child...", creationDatetime, deadline, group);
        var actual = activityFactory.AddChild(task, child);
        
        Assert.That(actual.Title == "Test");
        Assert.That(actual.Description == "Test...");
        Assert.That(child.Parent == task);
    }
}