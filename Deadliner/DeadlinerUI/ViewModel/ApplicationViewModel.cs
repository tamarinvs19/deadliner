using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Deadliner.Api.Storage;
using DeadlinerUI.Models;

namespace DeadlinerUI.ViewModel;

public class ApplicationViewModel : INotifyPropertyChanged
{
    private readonly IStorage<Calendar> _calendars;
    private Calendar? _selectedCalendar;

    public ObservableCollection<Calendar> Calendars { get; set; }

    public Calendar? SelectedCalendar
    {
        get => _selectedCalendar;
        set
        {
            if (value != null) _selectedCalendar = _calendars.Get(value.Id);

            OnPropertyChanged("SelectedCalendar");
        }
    }
    
    public ApplicationViewModel(IStorage<Calendar> calendars)
    {
        _calendars = calendars;

        Calendars = new ObservableCollection<Calendar>();
        foreach (var calendar in _calendars.Items())
        {
            Calendars.Add(calendar);
        }

        if (Calendars.Count > 0)
        {
            _selectedCalendar = _calendars.Get(Calendars[0].Id);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName]string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}