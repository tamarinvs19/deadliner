using System;
using System.Windows;
using System.Windows.Controls;
using Deadliner;
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
                var newAction = context.SelectedAction;
                newAction.Deadline = (DateTime)ActionDeadline.SelectedDate;
                newAction.CreationDateTime = (DateTime)ActionStart.SelectedDate;
                newAction.Title = ActionTitle.Text;
                newAction.Description = ActionDescription.Text;
                context.UpdateAction(newAction);
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
