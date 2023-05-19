using System;
using System.Windows;
using System.Windows.Controls;
using Deadliner;
using Deadliner.Models;
using Deadliner.Models.LocalActionStates;
using DeadlinerUI.ViewModel;

namespace DeadlinerUI.View
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Tasks : Page
    {
        public Tasks()
        {
            InitializeComponent();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            TasksVm context = (TasksVm)DataContext;
            if (context.SelectedAction != null)
            {
                context.SelectedAction.Deadline = (DateTime)ActionDeadline.SelectedDate;
                context.SelectedAction.CreationDateTime = (DateTime)ActionStart.SelectedDate;
                context.SelectedAction.Title = ActionTitle.Text;
                context.SelectedAction.Description = ActionDescription.Text;
                context.UpdateAction(context.SelectedAction);
            }
        }
        
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            TasksVm context = (TasksVm)DataContext;
            if (context.SelectedAction != null)
            {
                context.DeleteAction(context.SelectedAction.Id);
            }
        }
        
        private void Button_Create(object sender, RoutedEventArgs e)
        {
            TasksVm context = (TasksVm)DataContext;
            var mainContext = MainContainer.Context();
            var defaultGroup = mainContext.Groups.Get(1);
            var newAction = new ActivityFactory().MakeTask(
                ActionTitle.Text,
                ActionDescription.Text,
                (DateTime)ActionStart.SelectedDate,
                (DateTime)ActionDeadline.SelectedDate,
                defaultGroup
                );
            context.CreateAction(newAction);
        }
    }
}
