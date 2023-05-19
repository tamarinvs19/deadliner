using System.Collections.ObjectModel;
using System.Linq;
using Deadliner;
using Deadliner.Api.Controller;
using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Models;
using Deadliner.Models.LocalActionStates;
using DeadlinerUI.Utilities;

namespace DeadlinerUI.ViewModel;

class TasksVm : ViewModelBase
{
    private ILocalTask? _selectedAction;
    private IStorage<ILocalTask> _localActions;
    private readonly IContext _context;
    public ObservableCollection<ILocalTask> LocalActions { get; set; }

    public TasksVm()
    {
        _context = MainContainer.Context();
        _initTasks();
    }

    private void _initTasks()
    {
        _localActions = CalendarBuilderUI.BuildTasksList(1, _context).LocalTasks;
        var localActions = _localActions.Items().ToList();

        LocalActions = new ObservableCollection<ILocalTask>(localActions);
        if (localActions.Count > 0)
        {
            _selectedAction = localActions.First();
        }
    }

    public void UpdateAction(ILocalTask task)
    {
        _context.LocalTasks.Update(task);
        _initTasks();
        OnPropertyChanged();
    } 
    
    public void CreateAction(ILocalTask task)
    {
        _context.LocalTasks.Create(task);
        var user = _context.Users.Get(1);
        _context.UserToLocalAction.Create(new UserToLocalAction(user, task, new ActualState()));
        _initTasks();
        OnPropertyChanged();
    } 
    
    public void DeleteAction(int taskId)
    {
        var userToLocalActionId = _context.UserToLocalAction.Items().First(it => it.LocalAction.Id == taskId && it.User.Id == 1).Id;
        _context.UserToLocalAction.Delete(userToLocalActionId);
        _context.LocalTasks.Delete(taskId);
        _initTasks();
        OnPropertyChanged();
    } 

    public ILocalTask? SelectedAction
    {
        get => _selectedAction;
        set
        {
            if (value != null)
            {
                _selectedAction = _localActions.Get(value.Id);
            }
            OnPropertyChanged("SelectedAction");
        }
    }

}