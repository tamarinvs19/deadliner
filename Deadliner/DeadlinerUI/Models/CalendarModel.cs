using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using Deadliner.Api.Models;
using Deadliner.Api.Storage;
using Deadliner.Storage;

namespace DeadlinerUI.Models;

public class CalendarModel : INotifyPropertyChanged, IObject
{
    private readonly DateTime? _dateTime;
    private ILocalAction? _selectedAction;
    private readonly IStorage<ILocalAction> _localActions;
    public ObservableCollection<ILocalAction> LocalActions { get; set; }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    public CalendarModel(Deadliner.Models.Calendar calendar)
    {
        Id = calendar.Id;
        _localActions = new MemoryStorage<ILocalAction>();
        foreach (var localAction in calendar.LocalActions())
        {
            _localActions.Create(localAction);
        }
        LocalActions = new ObservableCollection<ILocalAction>(calendar.LocalActions());
        _dateTime = calendar.DateTime;
        if (calendar.LocalActions().Count > 0)
        {
            _selectedAction = _localActions.Items().First();
        }
    }
    
    public ILocalAction? SelectedAction
    {
        get => _selectedAction;
        set
        {
            if (value != null) _selectedAction = _localActions.Get(value.Id);

            OnPropertyChanged("SelectedAction");
        }
    }

    public int Id { get; private set; }

    public string? DateTime
    {
        get
        {
            if (_dateTime != null)
            {
                var dt = (System.DateTime)_dateTime;
                var pattern = "MMM d ddd";
                return dt.ToString(pattern);
            }

            return null;
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}