using Atlas.UI.Events;
using Atlas.UI.Extensions;
using Atlas.UI.WindowStates;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Atlas.UI
{
    public class Window : System.Windows.Window, INotifyPropertyChanged
    {
        private System.Windows.Controls.Button CloseButton { get; set; }
        private System.Windows.Controls.Button MaximizeButton { get; set; }
        private System.Windows.Controls.Button MinimizeButton { get; set; }
        private System.Windows.Controls.Button ShadeButton { get; set; }

        private double PreviousHeight { get; set; }
        private WindowStyle PreviousStyle { get; set; }

        private Border CaptionBorder { get; set; }
        private Border MainBorder { get; set; }

        public static readonly DependencyProperty ShadeStateProperty = DependencyProperty.Register(
            nameof(ShadeState),
            typeof(ShadeState),
            typeof(Window)
        );

        public static readonly DependencyProperty CaptionMenuProperty = DependencyProperty.RegisterAttached(
            nameof(CaptionMenu),
            typeof(List<MenuItem>),
            typeof(Window)
        );

        public static readonly DependencyProperty ShowCaptionBorderProperty = DependencyProperty.Register(
            nameof(ShowCaptionBorder),
            typeof(bool),
            typeof(Window)
        );

        public static readonly DependencyProperty CanMaximizeProperty = DependencyProperty.Register(
            nameof(CanMaximize),
            typeof(bool),
            typeof(Window)
        );

        public static readonly DependencyProperty CanShadeProperty = DependencyProperty.Register(
            nameof(CanShade),
            typeof(bool),
            typeof(Window)
        );

        public static readonly DependencyProperty CanResizeProperty = DependencyProperty.Register(
            nameof(CanResize),
            typeof(bool),
            typeof(Window)
        );

        public static readonly DependencyProperty ShowCloseButtonProperty = DependencyProperty.Register(
            nameof(ShowCloseButton),
            typeof(bool),
            typeof(Window)
        );

        public static readonly DependencyProperty ShowShadeButtonProperty = DependencyProperty.Register(
            nameof(ShowShadeButton),
            typeof(bool),
            typeof(Window)
        );

        public static readonly DependencyProperty ShowMinimizeButtonProperty = DependencyProperty.Register(
            nameof(ShowMinimizeButton),
            typeof(bool),
            typeof(Window)
        );

        public static readonly DependencyProperty ShowMaximizeButtonProperty = DependencyProperty.Register(
            nameof(ShowMaximizeButton),
            typeof(bool),
            typeof(Window)
        );

        public ShadeState ShadeState
        {
            get { return (ShadeState)GetValue(ShadeStateProperty); }
            set
            {
                var currentValue = (ShadeState)GetValue(ShadeStateProperty);
                if (currentValue == value) return;

                if (value == ShadeState.Shaded)
                {
                    PreviousHeight = Height;
                    
                    Height = 31;
                    MainBorder.Height = 31;

                    this.SetBorder(false);
                    this.SetResizing(false);
                    CanMaximize = false;
                }
                else
                {
                    MainBorder.Height = double.NaN;
                    Height = PreviousHeight;
                    

                    this.SetBorder(true);
                    this.SetResizing(true);
                    CanMaximize = true;
                }

                SetValue(ShadeStateProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShadeState)));
                ShadeStateChanged?.Invoke(this, new ShadeStateChangedEventArgs(value));
            }
        }

        public List<MenuItem> CaptionMenu
        {
            get { return (List<MenuItem>)GetValue(CaptionMenuProperty); }
            set
            {
                SetValue(CaptionMenuProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CaptionMenu)));
            }
        }

        public bool CanMaximize
        {
            get { return (bool)GetValue(CanMaximizeProperty); }
            set
            {
                this.SetMaximization(value);

                SetValue(CanMaximizeProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanMaximize)));
            }
        }
        public bool CanShade
        {
            get { return (bool)GetValue(CanShadeProperty); }
            set
            {
                SetValue(CanShadeProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanShade)));
            }
        }

        public bool CanResize
        {
            get { return (bool)GetValue(CanResizeProperty); }
            set
            {
                this.SetResizing(value);

                SetValue(CanResizeProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CanResize)));
            }
        }

        public bool ShowCaptionBorder
        {
            get { return (bool)GetValue(ShowCaptionBorderProperty); }
            set
            {
                SetValue(ShowCaptionBorderProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowCaptionBorder)));
            }
        }

        public bool ShowMaximizeButton
        {
            get { return (bool)GetValue(ShowMaximizeButtonProperty); }
            set
            {
                SetValue(ShowMaximizeButtonProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowMaximizeButton)));
            }
        }

        public bool ShowShadeButton
        {
            get { return (bool)GetValue(ShowShadeButtonProperty); }
            set
            {
                SetValue(ShowShadeButtonProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowShadeButton)));
            }
        }

        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set
            {
                SetValue(ShowCloseButtonProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowCloseButton)));
            }
        }

        public bool ShowMinimizeButton
        {
            get { return (bool)GetValue(ShowMinimizeButtonProperty); }
            set
            {
                SetValue(ShowMinimizeButtonProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowMinimizeButton)));
            }
        }

        public event System.EventHandler<ShadeStateChangedEventArgs> ShadeStateChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        static Window()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));
        }

        public Window()
        {
            CaptionMenu = new List<MenuItem>();
            SourceInitialized += Window_SourceInitialized;
        }

        private void Window_SourceInitialized(object sender, System.EventArgs e)
        {
            this.SetMaximization(CanMaximize);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            CloseButton = GetTemplateChild("PART_Close") as System.Windows.Controls.Button;
            MaximizeButton = GetTemplateChild("PART_Maximize") as System.Windows.Controls.Button;
            MinimizeButton = GetTemplateChild("PART_Minimize") as System.Windows.Controls.Button;
            ShadeButton = GetTemplateChild("PART_Shade") as System.Windows.Controls.Button;

            CaptionBorder = GetTemplateChild("PART_Caption") as Border;
            MainBorder = GetTemplateChild("PART_MainBorder") as Border;

            if (CloseButton != null)
                CloseButton.Click += CloseButton_Click;

            if (MaximizeButton != null)
                MaximizeButton.Click += MaximizeButton_Click;

            if (MinimizeButton != null)
                MinimizeButton.Click += MinimizeButton_Click;

            if (ShadeButton != null)
                ShadeButton.Click += ShadeButton_Click;

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

            if (e.ClickCount == 2 && CanMaximize)
            {
                ToggleMaximizedState();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            OnMinimizeButtonClicked(sender, e);
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            OnMaximizeButtonClicked(sender, e);
        }

        private void ShadeButton_Click(object sender, RoutedEventArgs e)
        {
            OnShadeButtonClicked(sender, e);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            OnCloseButtonClicked(sender, e);
        }

        private void ToggleMaximizedState()
        {
            if (WindowState == WindowState.Maximized)
            {
                CanShade = true;

                MainBorder.Margin = new Thickness(0);
                WindowState = WindowState.Normal;
            }
            else
            {
                CanShade = false;

                MainBorder.Margin = new Thickness(6);
                WindowState = WindowState.Maximized;
            }
        }

        private void ToggleShadedState()
        {
            if (ShadeState == ShadeState.Shaded)
            {
                ShadeState = ShadeState.Unshaded;
            }
            else
            {
                ShadeState = ShadeState.Shaded;
            }
        }

        protected virtual void OnCloseButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected virtual void OnMaximizeButtonClicked(object sender, RoutedEventArgs e)
        {
            ToggleMaximizedState();
        }

        protected virtual void OnMinimizeButtonClicked(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        protected virtual void OnShadeButtonClicked(object sender, RoutedEventArgs e)
        {
            ToggleShadedState();
        }
    }
}
