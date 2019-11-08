using System.Windows;
using FibUhr.ViewModels;

namespace FibUhr
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class FibonacciClock : Window
    {
        public FibonacciClock()
        {
            InitializeComponent();
            this.DataContext = new FibonacciClockViewModel();
        }
    }
}
