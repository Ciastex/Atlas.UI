using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Atlas.UI
{
    public class Window : System.Windows.Window
    {
        private System.Windows.Controls.Button CloseButton { get; set; }
        private System.Windows.Controls.Button MaximizeButton { get; set; }
        private System.Windows.Controls.Button MinimizeButton { get; set; }
        private Border CaptionBorder { get; set; }
        private Border MainBorder { get; set; }

        public static readonly DependencyProperty CaptionMenuProperty = DependencyProperty.RegisterAttached("CaptionMenu", typeof(List<MenuItem>), typeof(Window));
        public static readonly DependencyProperty ShowCaptionBorderProperty = DependencyProperty.Register("ShowCaptionBorder", typeof(bool), typeof(Window));

        public List<MenuItem> CaptionMenu
        {
            get { return GetValue(CaptionMenuProperty) as List<MenuItem>; }
            set { SetValue(CaptionMenuProperty, value); }
        }

        public bool ShowCaptionBorder
        {
            get { return (bool)GetValue(ShowCaptionBorderProperty); }
            set { SetValue(ShowCaptionBorderProperty, value);}
        }

        static Window()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));
        }

        public Window()
        {
            CaptionMenu = new List<MenuItem>();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            CloseButton = GetTemplateChild("PART_Close") as System.Windows.Controls.Button;
            MaximizeButton = GetTemplateChild("PART_Maximize") as System.Windows.Controls.Button;
            MinimizeButton = GetTemplateChild("PART_Minimize") as System.Windows.Controls.Button;
            CaptionBorder = GetTemplateChild("PART_Caption") as Border;
            MainBorder = GetTemplateChild("PART_MainBorder") as Border;

            if (CloseButton != null)
                CloseButton.Click += CloseButton_Click;

            if (MaximizeButton != null)
                MaximizeButton.Click += MaximizeButton_Click;

            if (MinimizeButton != null)
                MinimizeButton.Click += MinimizeButton_Click;

            if (CaptionBorder != null)
                CaptionBorder.MouseDown += Border_MouseDown;
        }

        public void SetWindowBorderColor(Color color)
        {
            Application.Current?.Dispatcher.Invoke(() =>
            {
                MainBorder.BorderBrush = new SolidColorBrush(color);
            });
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }

            if (e.ClickCount == 2)
            {
                ToggleMaximizedState();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleMaximizedState();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ToggleMaximizedState()
        {
            if (WindowState == WindowState.Maximized)
            {
                MainBorder.Margin = new Thickness(0);
                WindowState = WindowState.Normal;
            }
            else
            {
                MainBorder.Margin = new Thickness(6);
                WindowState = WindowState.Maximized;
            }
        }
    }
}
