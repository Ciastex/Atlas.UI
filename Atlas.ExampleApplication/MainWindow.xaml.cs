using Atlas.UI.Systems;
using System.Windows;

namespace Atlas.ExampleApplication
{
    public partial class MainWindow
    {
        public MainWindow()
        {

            InitializeComponent();
            var evs = EventManager.GetRoutedEvents();

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Progressbar.Maximum = 12;
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            SingleInstanceWindowManager.OpenOrActivate<SingleWindow>(this);
        }

        private void Button_Click_3(object sender, System.Windows.RoutedEventArgs e)
        {
            TestSpinner.IsTaskRunning = !TestSpinner.IsTaskRunning;
        }
    }
}
