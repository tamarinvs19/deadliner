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

class EventsVm : ViewModelBase
{
    private ILocalEvent? _selectedAction;
    private IStorage<ILocalEvent> _localActions;
    private readonly IContext _context;
    public ObservableCollection<ILocalEvent> LocalActions { get; set; }

    public EventsVm()
    {
        _context = MainContainer.Context();
        _initEvents();
    }

    private void _initEvents()
    {
        _localActions = CalendarBuilderUI.BuildEventsList(1).LocalEvents;
        var localActions = _localActions.Items().ToList();

        LocalActions = new ObservableCollection<ILocalEvent>(localActions);
        if (localActions.Count > 0)
        {
            _selectedAction = localActions.First();
        }
    }

    public ILocalEvent? SelectedAction
    {
        get => _selectedAction;
        set
        {
            if (value != null) _selectedAction = _localActions.Get(value.Id);

            OnPropertyChanged("SelectedAction");
        }
    }
    
    public void UpdateAction(ILocalEvent action)
    {
        _context.LocalEvents.Update(action);
        _initEvents();
        OnPropertyChanged();
    } 
    
    public void CreateAction(ILocalEvent action)
    {
        _context.LocalEvents.Create(action);
        var user = _context.Users.Get(1);
        _context.UserToLocalAction.Create(new UserToLocalAction(
            MainContainer.IdGenerator().NextId(), user, action, new ActualState())
        );
        _initEvents();
        OnPropertyChanged();
    } 
    
    public void DeleteAction(int eventId)
    {
        var userToLocalActionId = _context.UserToLocalAction.Items().First(it => it.LocalAction.Id == eventId && it.User.Id == 1).Id;
        _context.UserToLocalAction.Delete(userToLocalActionId);
        _context.LocalEvents.Delete(eventId);
        _initEvents();
        OnPropertyChanged();
    } 
}