using System;
using System.Windows;
using System.Windows.Input;
using Deadliner;
using Deadliner.Api.Controller;
using Deadliner.Controller;
using Deadliner.Storage;
using DeadlinerUI.Models;
using DeadlinerUI.Utilities;

namespace DeadlinerUI.ViewModel
{
    class NavigationVm : ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand CalendarCommand { get; set; }
        public ICommand TasksCommand { get; set; }
        public ICommand EventsCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();

        private void Calendar(object obj) => CurrentView = new CalendarVm();  
        
        private void Tasks(object obj) => CurrentView = new TasksVm();
        
        private void Events(object obj) => CurrentView = new EventsVm();

        public NavigationVm()
        {
            HomeCommand = new RelayCommand(Home);
            CalendarCommand = new RelayCommand(Calendar);
            TasksCommand = new RelayCommand(Tasks);
            EventsCommand = new RelayCommand(Events);

            // Startup Page
            _currentView = new HomeVM();
        }
    }
}
