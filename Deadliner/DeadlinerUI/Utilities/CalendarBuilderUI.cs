using System;
using Deadliner;
using Deadliner.Api.Controller;
using Deadliner.Api.Storage;
using Deadliner.Controller;
using Deadliner.Storage;
using DeadlinerUI.Models;

namespace DeadlinerUI.Utilities;

public static class CalendarBuilderUI
{
    public static IStorage<CalendarModel> BuildCalendars(int userId, IContext context)
    {
            var user = context.Users.Get(userId);
            var tasks = context.LocalTasks.Items();
            var events = context.LocalEvents.Items();
            
            var calendarBuilder = new CalendarBuilder(context, user);
            foreach (var task in tasks)
            {
                calendarBuilder.AddLocalAction(task);
            }
            foreach (var localEvent in events)
            {
                calendarBuilder.AddLocalAction(localEvent);
            }
            
            var today = context.TimeProvider.Today();
            var calendars = calendarBuilder.DaySplitBuild(today - TimeSpan.FromDays(5), today + TimeSpan.FromDays(20));
            var storage = new MemoryStorage<CalendarModel>();
            foreach (var calendar in calendars)
            {
                storage.Create(new CalendarModel((Deadliner.Models.Calendar)calendar));
            }

            return storage;
    }
    
    public static LocalTasksModel BuildTasksList(int userId, IContext context)
    {
            var user = context.Users.Get(userId);
            var tasks = context.LocalTasks.Items();
            
            var calendarBuilder = new CalendarBuilder(context, user);
            foreach (var task in tasks)
            {
                calendarBuilder.AddLocalAction(task);
            }

            var calendar = calendarBuilder.Build();
            return new LocalTasksModel(calendar, context);
    }
    
    public static LocalEventsModel BuildEventsList(int userId)
    {
            var context = new MyDeadlinerContext();
            var user = context.Users.Get(userId);
            var events = context.LocalEvents.Items();
            
            var calendarBuilder = new CalendarBuilder(context, user);
            foreach (var localEvent in events)
            {
                calendarBuilder.AddLocalAction(localEvent);
            }

            var calendar = calendarBuilder.Build();
            return new LocalEventsModel(calendar);
    }
}