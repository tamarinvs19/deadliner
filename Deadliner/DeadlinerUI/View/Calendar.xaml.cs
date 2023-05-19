using System.Windows.Controls;
using System.Windows.Input;
using DeadlinerUI.ViewModel;

namespace DeadlinerUI.View
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Calendar : Page
    {
        public Calendar()
        {
            InitializeComponent();
            DataContext = new CalendarVm();
        }

        private void Calendar_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // throw new System.NotImplementedException();
        }

        private void Action_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            // throw new System.NotImplementedException();
        }
    }
}
