using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Deadliner;
using Deadliner.Controller;
using Deadliner.Storage;
using DeadlinerUI.Models;
using DeadlinerUI.ViewModel;

namespace DeadlinerUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContainer.BuildContainer();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
// public MainWindow()
    // {
    //     InitializeComponent();
    //
    //     var context = new MyDeadlinerContext();
    //     var user = context.Users.Get(1);
    //     var tasks = context.LocalTasks.Items();
    //     var events = context.LocalEvents.Items();
    //
    //     var calendarBuilder = new CalendarBuilder(context, user);
    //     foreach (var task in tasks)
    //     {
    //         calendarBuilder.AddLocalAction(task);
    //     }
    //     foreach (var localEvent in events)
    //     {
    //         calendarBuilder.AddLocalAction(localEvent);
    //     }
    //
    //     var today = context.TimeProvider.Today();
    //     var calendars = calendarBuilder.DaySplitBuild(today - TimeSpan.FromDays(5), today + TimeSpan.FromDays(20));
    //     var storage = new MemoryStorage<Calendar>();
    //     foreach (var calendar in calendars)
    //     {
    //         storage.Create(new Calendar((Deadliner.Models.Calendar)calendar));
    //     }
    //     DataContext = new ApplicationViewModel(storage);
    // }
    //
    // private void Calendar_OnMouseDown(object sender, MouseButtonEventArgs e)
    // {
    //     Console.WriteLine(sender);
    // }
    //
    // private void Action_OnMouseDown(object sender, MouseButtonEventArgs e)
    // {
    //     Console.WriteLine(sender);
    // }
// }