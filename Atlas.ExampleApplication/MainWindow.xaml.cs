using Atlas.UI.Enums;
using Atlas.UI.Systems;
using System.Diagnostics;
using System.Windows;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Progressbar.Value = 6;
            Progressbar.Maximum = 12;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SingleInstanceWindowManager.OpenOrActivateDialog<SingleWindow>(this, WindowStartupLocation.CenterScreen);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            TestSpinner.IsTaskRunning = !TestSpinner.IsTaskRunning;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Progressbar.IsIndeterminate = false;
            Progressbar.ProgressTextTemplate = "%val% / %max%";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            new MessageBox()
                .Titled("Important message!")
                .WithMessage(new StackTrace().ToString())
                .WithAdditionalDescription("Do you want to report this to developers?")
                .WithButtons(MessageBoxButtons.Yes | MessageBoxButtons.No)
                .OkClickExecutes(() => TestSpinner.IsTaskRunning = false)
                .WhenClosedAbnormally(() => Debug.WriteLine("Dialog closed abnormally."))
                .OwnedBy(this)
                .CenterOwner()
                .Show();

            new MessageBox()
                .WithMessage("This is a short message to show simple variant of message display.")
                .OwnedBy(this)
                .CenterScreen()
                .Show();
        }

        private void PasswordBox_TEST_PasswordEntered(object sender, UI.Events.PasswordInputEntryEventArgs e)
        {
            new MessageBox()
                .WithMessage(new string(e.Password))
                .WithButtons(MessageBoxButtons.Ok)
                .OwnedBy(this)
                .CenterOwner()
                .Show();
        }
    }
}
