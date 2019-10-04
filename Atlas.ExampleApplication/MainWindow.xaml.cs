using Atlas.UI.Enums;
using Atlas.UI.Systems;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Media;
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

        private void Toggle_Checked(object sender, RoutedEventArgs e)
        {
            CanResize = true;
        }

        private void Toggle_Unchecked(object sender, RoutedEventArgs e)
        {
            CanResize = false;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                double v = 0;

                while (true)
                {
                    v++;
                    v %= 360;

                    SetWindowGlowColor(ColorFromHSV(v, 1, 1));
                    SetWindowBorderColor(ColorFromHSV(v, 1, 1));

                    Thread.Sleep(1);
                }
            }).Start();
        }

        private static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value *= 255;
            byte v = Convert.ToByte(value);
            byte p = Convert.ToByte(value * (1 - saturation));
            byte q = Convert.ToByte(value * (1 - f * saturation));
            byte t = Convert.ToByte(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}
