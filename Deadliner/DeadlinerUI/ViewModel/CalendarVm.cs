using System;
using System.Collections.ObjectModel;
using Deadliner;
using Deadliner.Api.Controller;
using Deadliner.Api.Storage;
using Deadliner.Controller;
using Deadliner.Storage;
using DeadlinerUI.Models;
using DeadlinerUI.Utilities;

namespace DeadlinerUI.ViewModel;

class CalendarVm : ViewModelBase
{
    private readonly IStorage<CalendarModel> _calendars;
    private CalendarModel? _selectedCalendar;
    private readonly IContext _context;

    public ObservableCollection<CalendarModel> Calendars { get; set; }

    public CalendarModel? SelectedCalendar
    {
        get => _selectedCalendar;
        set
        {
            if (value != null) _selectedCalendar = _calendars.Get(value.Id);
            OnPropertyChanged();
        }
    }

    public CalendarVm()
    {
        _context = MainContainer.Context();
        
        _calendars = CalendarBuilderUI.BuildCalendars(1, _context);

        Calendars = new ObservableCollection<CalendarModel>();
        foreach (var calendar in _calendars.Items())
        {
            Calendars.Add(calendar);
        }

        if (Calendars.Count > 0)
        {
            _selectedCalendar = _calendars.Get(Calendars[0].Id);
        }
    }
}