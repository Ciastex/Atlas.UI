using Atlas.UI;

namespace Atlas.ExampleApplication
{
    public partial class SingleWindow
    {
        public SingleWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ActivitySpinner.IsTaskRunning = !ActivitySpinner.IsTaskRunning;
        }
    }
}
