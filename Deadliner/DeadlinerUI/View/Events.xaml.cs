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
    public partial class Events : Page
    {
        public Events()
        {
            InitializeComponent();
        }
        
        private void Button_Save(object sender, RoutedEventArgs e)
        {
            var context = (EventsVm)DataContext;
            if (context.SelectedAction != null)
            {
                context.SelectedAction.DateTime = (DateTime)ActionDatetime.SelectedDate;
                context.SelectedAction.Title = ActionTitle.Text;
                context.SelectedAction.Description = ActionDescription.Text;
                context.UpdateAction(context.SelectedAction);
            }
        }
        
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            var context = (EventsVm)DataContext;
            if (context.SelectedAction != null)
            {
                context.DeleteAction(context.SelectedAction.Id);
            }
        }
        
        private void Button_Create(object sender, RoutedEventArgs e)
        {
            var context = (EventsVm)DataContext;
            var mainContext = MainContainer.Context();
            var defaultGroup = mainContext.Groups.Get(1);
            var newAction = new ActivityFactory().MakeLocalEvent(
                ActionTitle.Text,
                ActionDescription.Text,
                (DateTime)ActionDatetime.SelectedDate,
                defaultGroup
                );
            context.CreateAction(newAction);
        }
    }
}
