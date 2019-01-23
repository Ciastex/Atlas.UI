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
    }
}
