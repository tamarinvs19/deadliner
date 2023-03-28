using Deadliner;
using Deadliner.Controller;
using Deadliner.Models;
using Deadliner.Models.LocalActionStates;
using NUnit.Framework;

namespace DeadlinerTests.Builders;

[TestFixture]
public class CalendarBuilderTest
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
    
    [Test]
    public void TestAddSuperGroup()
    {
        var context = new BaseContext();
        var owner = new UserStub(1, "A", "B");
        var superGroup1 = new SuperGroupStub(2, "Super #1", "Super...", null, owner);
        var superGroup2 = new SuperGroupStub(20, "Super #2", "Super...", null, owner);
        context.SuperGroups.Create(superGroup1);
        context.SuperGroups.Create(superGroup2);
        var group1 = new GroupStub(3, "Group #1", "Group...", null, owner, superGroup1);
        var group2 = new GroupStub(30, "Group #2", "Group...", null, owner, superGroup2);
        context.Groups.Create(group1);
        context.Groups.Create(group2);
        var localEvent1 = new LocalEvent("Event #1", "Task...", DateTime.Today, group1, new OverdueState());
        var localEvent2 = new LocalEvent("Event #2", "Task...", DateTime.Today, group2, new OverdueState());
        context.LocalEvents.Create(localEvent1);
        context.LocalEvents.Create(localEvent2);
        var user = new UserStub(2, "user", "0");
        context.Users.Create(user);

        var userInSuperGroup1 = new UserToSuperGroup(user, superGroup1);
        var userInGroup1 = new UserToGroup(user, group1);
        context.UserToSuperGroup.Create(userInSuperGroup1);
        context.UserToGroup.Create(userInGroup1);
        
        var builder = new CalendarBuilder(context, user);
        builder.AddSuperGroup(superGroup1);
        var actual = builder.Build();
        
        Assert.True(actual.LocalEvents.Contains(localEvent1));
        Assert.False(actual.LocalEvents.Contains(localEvent2));
    }    
    
    [Test]
    public void TestAddGroup()
    {
        var context = new BaseContext();
        var owner = new UserStub(1, "A", "B");
        var superGroup1 = new SuperGroupStub(2, "Super #1", "Super...", null, owner);
        var superGroup2 = new SuperGroupStub(20, "Super #2", "Super...", null, owner);
        context.SuperGroups.Create(superGroup1);
        context.SuperGroups.Create(superGroup2);
        var group1 = new GroupStub(3, "Group #1", "Group...", null, owner, superGroup1);
        var group2 = new GroupStub(30, "Group #2", "Group...", null, owner, superGroup2);
        context.Groups.Create(group1);
        context.Groups.Create(group2);
        var localEvent1 = new LocalEvent("Event #1", "Task...", DateTime.Today, group1, new OverdueState());
        var localEvent2 = new LocalEvent("Event #2", "Task...", DateTime.Today, group2, new OverdueState());
        context.LocalEvents.Create(localEvent1);
        context.LocalEvents.Create(localEvent2);
        var user = new UserStub(2, "user", "0");
        context.Users.Create(user);

        var userInSuperGroup1 = new UserToSuperGroup(user, superGroup1);
        var userInGroup1 = new UserToGroup(user, group1);
        context.UserToSuperGroup.Create(userInSuperGroup1);
        context.UserToGroup.Create(userInGroup1);
        
        var builder = new CalendarBuilder(context, user);
        builder.AddGroup(group1);
        var actual = builder.Build();
        
        Assert.True(actual.LocalEvents.Contains(localEvent1));
        Assert.False(actual.LocalEvents.Contains(localEvent2));
    }    
    
    [Test]
    public void TestAddLocalActionValid()
    {
        var context = new BaseContext();
        var owner = new UserStub(1, "A", "B");
        var superGroup1 = new SuperGroupStub(2, "Super #1", "Super...", null, owner);
        var superGroup2 = new SuperGroupStub(20, "Super #2", "Super...", null, owner);
        context.SuperGroups.Create(superGroup1);
        context.SuperGroups.Create(superGroup2);
        var group1 = new GroupStub(3, "Group #1", "Group...", null, owner, superGroup1);
        var group2 = new GroupStub(30, "Group #2", "Group...", null, owner, superGroup2);
        context.Groups.Create(group1);
        context.Groups.Create(group2);
        var localEvent1 = new LocalEvent("Event #1", "Task...", DateTime.Today, group1, new OverdueState());
        var localEvent2 = new LocalEvent("Event #2", "Task...", DateTime.Today, group2, new OverdueState());
        context.LocalEvents.Create(localEvent1);
        context.LocalEvents.Create(localEvent2);
        var user = new UserStub(2, "user", "0");
        context.Users.Create(user);

        var userInSuperGroup1 = new UserToSuperGroup(user, superGroup1);
        var userInGroup1 = new UserToGroup(user, group1);
        context.UserToSuperGroup.Create(userInSuperGroup1);
        context.UserToGroup.Create(userInGroup1);

        var userToLocalAction = new UserToLocalAction(user, localEvent1, new OverdueState());
        context.UserToLocalAction.Create(userToLocalAction);
        
        var builder = new CalendarBuilder(context, user);
        builder.AddLocalAction(localEvent1);
        var actual = builder.Build();
        
        Assert.True(actual.LocalEvents.Contains(localEvent1));
        Assert.False(actual.LocalEvents.Contains(localEvent2));
    }    
    
    [Test]
    public void TestAddLocalActionInvalidAction()
    {
        var context = new BaseContext();
        var owner = new UserStub(1, "A", "B");
        var superGroup1 = new SuperGroupStub(2, "Super #1", "Super...", null, owner);
        var superGroup2 = new SuperGroupStub(20, "Super #2", "Super...", null, owner);
        context.SuperGroups.Create(superGroup1);
        context.SuperGroups.Create(superGroup2);
        var group1 = new GroupStub(3, "Group #1", "Group...", null, owner, superGroup1);
        var group2 = new GroupStub(30, "Group #2", "Group...", null, owner, superGroup2);
        context.Groups.Create(group1);
        context.Groups.Create(group2);
        var localEvent1 = new LocalEvent("Event #1", "Task...", DateTime.Today, group1, new OverdueState());
        var localEvent2 = new LocalEvent("Event #2", "Task...", DateTime.Today, group2, new OverdueState());
        context.LocalEvents.Create(localEvent1);
        context.LocalEvents.Create(localEvent2);
        var user = new UserStub(2, "user", "0");
        context.Users.Create(user);

        var userInSuperGroup1 = new UserToSuperGroup(user, superGroup1);
        var userInGroup1 = new UserToGroup(user, group1);
        context.UserToSuperGroup.Create(userInSuperGroup1);
        context.UserToGroup.Create(userInGroup1);
        
        var userToLocalAction = new UserToLocalAction(user, localEvent1, new OverdueState());
        context.UserToLocalAction.Create(userToLocalAction);
        
        var builder = new CalendarBuilder(context, user);
        builder.AddLocalAction(localEvent2);
        var actual = builder.Build();
        
        Assert.False(actual.LocalEvents.Contains(localEvent1));
        Assert.False(actual.LocalEvents.Contains(localEvent2));
    }    
    
    [Test]
    public void TestAddFilterParentAction()
    {
        var context = new BaseContext();
        var owner = new UserStub(1, "A", "B");
        var superGroup1 = new SuperGroupStub(2, "Super #1", "Super...", null, owner);
        context.SuperGroups.Create(superGroup1);
        var group1 = new GroupStub(3, "Group #1", "Group...", null, owner, superGroup1);
        context.Groups.Create(group1);
        var localEvent1 = new LocalEvent("Event #1", "Task...", DateTime.Today, group1, new OverdueState());
        var localEvent2 = new LocalEvent("Event #2", "Task...", DateTime.Today, group1, new OverdueState());
        localEvent1.Parent = localEvent2;
        context.LocalEvents.Create(localEvent1);
        context.LocalEvents.Create(localEvent2);
        var user = new UserStub(2, "user", "0");
        context.Users.Create(user);

        var userInSuperGroup1 = new UserToSuperGroup(user, superGroup1);
        var userInGroup1 = new UserToGroup(user, group1);
        context.UserToSuperGroup.Create(userInSuperGroup1);
        context.UserToGroup.Create(userInGroup1);
        
        var userToLocalAction1 = new UserToLocalAction(user, localEvent1, new OverdueState());
        var userToLocalAction2 = new UserToLocalAction(user, localEvent2, new OverdueState());
        context.UserToLocalAction.Create(userToLocalAction1);
        context.UserToLocalAction.Create(userToLocalAction2);
        
        var builder = new CalendarBuilder(context, user);
        builder.AddGroup(group1).FilterParentsLocalAction();
        var actual = builder.Build();
        
        Assert.False(actual.LocalEvents.Contains(localEvent1));
        Assert.True(actual.LocalEvents.Contains(localEvent2));
    }    
    
    [Test]
    public void TestAddFilterKind()
    {
        var context = new BaseContext();
        var owner = new UserStub(1, "A", "B");
        var superGroup1 = new SuperGroupStub(2, "Super #1", "Super...", null, owner);
        context.SuperGroups.Create(superGroup1);
        var group1 = new GroupStub(3, "Group #1", "Group...", null, owner, superGroup1);
        context.Groups.Create(group1);
        var localEvent1 = new LocalEvent("Event #1", "Task...", DateTime.Today, group1, new OverdueState());
        var localTask2 = new LocalTask("Task #2", "Task...", DateTime.Today, DateTime.Now, group1, new OverdueState());
        localEvent1.Parent = localTask2;
        context.LocalEvents.Create(localEvent1);
        context.LocalTasks.Create(localTask2);
        var user = new UserStub(2, "user", "0");
        context.Users.Create(user);

        var userInSuperGroup1 = new UserToSuperGroup(user, superGroup1);
        var userInGroup1 = new UserToGroup(user, group1);
        context.UserToSuperGroup.Create(userInSuperGroup1);
        context.UserToGroup.Create(userInGroup1);
        
        var userToLocalAction1 = new UserToLocalAction(user, localEvent1, new OverdueState());
        var userToLocalAction2 = new UserToLocalAction(user, localTask2, new OverdueState());
        context.UserToLocalAction.Create(userToLocalAction1);
        context.UserToLocalAction.Create(userToLocalAction2);
        
        var builder = new CalendarBuilder(context, user);
        builder.AddGroup(group1).FilterKindLocalAction<ILocalEvent>();
        var actual = builder.Build();
        
        Assert.True(actual.LocalEvents.Contains(localEvent1));
        Assert.False(actual.LocalTasks.Contains(localTask2));
    }    
}