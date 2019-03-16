using Atlas.UI.Enums;
using Atlas.UI.Systems;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using MessageBox = Atlas.UI.MessageBox;

namespace Atlas.ExampleApplication
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ShadeStateChanged += MainWindow_ShadeStateChanged;
        }

        private void MainWindow_ShadeStateChanged(object sender, UI.Events.ShadeStateChangedEventArgs e)
        {
            if (e.ShadeState == UI.WindowStates.ShadeState.Shaded)
                SearchTextBox.Visibility = Visibility.Collapsed;
            else
                SearchTextBox.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Progressbar.Maximum = 12;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SingleInstanceWindowManager.OpenOrActivate<SingleWindow>(this);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            TestSpinner.IsTaskRunning = !TestSpinner.IsTaskRunning;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Progressbar.IsIndeterminate = false;
        }

        private void Window_ShadeStateChanged(object sender, UI.Events.ShadeStateChangedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            new MessageBox()
                .Titled("Important message!")
                .WithMessage(new StackTrace().ToString())
                .WithButtons(MessageBoxButtons.Ok)
                .OkClickExecutes(() => TestSpinner.IsTaskRunning = false)
                .WhenClosedAbnormally(() => Debug.WriteLine("Dialog closed abnormally."))
                .OwnedBy(this)
                .Show();
        }
    }
}
