using Atlas.UI.Systems;

namespace Atlas.ExampleApplication
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Progressbar.Maximum = 12;
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            SingleInstanceWindowManager.OpenOrActivate<SingleWindow>(this);
        }
    }
}
