using Atlas.UI.Enums;
using Atlas.UI.Events;
using Atlas.UI.Extensions;
using Atlas.UI.WindowStates;
using System.Collections.ObjectModel;
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
        private bool PreviousCanMaximize { get; set; }

        private Border CaptionBorder { get; set; }
        private Border MainBorder { get; set; }

        public static readonly DependencyProperty ShadeStateProperty = Dependency.Register<ShadeState>(nameof(ShadeState));
        public static readonly DependencyProperty CaptionMenuProperty = Dependency.Register<ObservableCollection<MenuItem>>(nameof(CaptionMenu));
        public static readonly DependencyProperty ShowCaptionBorderProperty = Dependency.Register<bool>(nameof(ShowCaptionBorder));
        public static readonly DependencyProperty CanMaximizeProperty = Dependency.Register<bool>(nameof(CanMaximize));
        public static readonly DependencyProperty CanShadeProperty = Dependency.Register<bool>(nameof(CanShade));
        public static readonly DependencyProperty CanResizeProperty = Dependency.Register<bool>(nameof(CanResize));
        public static readonly DependencyProperty ShowCloseButtonProperty = Dependency.Register<bool>(nameof(ShowCloseButton));
        public static readonly DependencyProperty ShowShadeButtonProperty = Dependency.Register<bool>(nameof(ShowShadeButton));
        public static readonly DependencyProperty ShowMinimizeButtonProperty = Dependency.Register<bool>(nameof(ShowMinimizeButton));
        public static readonly DependencyProperty ShowMaximizeButtonProperty = Dependency.Register<bool>(nameof(ShowMaximizeButton));
        public static readonly DependencyProperty CaptionMenuAlignmentProperty = Dependency.Register<CaptionElementAlignment>(nameof(CaptionMenuAlignment));
        public static readonly DependencyProperty CaptionTitleAlignmentProperty = Dependency.Register<CaptionElementAlignment>(nameof(CaptionTitleAlignment));
        public static readonly DependencyProperty CaptionButtonsAlignmentProperty = Dependency.Register<CaptionElementAlignment>(nameof(CaptionButtonsAlignment));

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
                    PreviousCanMaximize = CanMaximize;

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
                    CanMaximize = PreviousCanMaximize;
                }

                SetValue(ShadeStateProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShadeState)));
                ShadeStateChanged?.Invoke(this, new ShadeStateChangedEventArgs(value));
            }
        }

        public ObservableCollection<MenuItem> CaptionMenu
        {
            get { return (ObservableCollection<MenuItem>)GetValue(CaptionMenuProperty); }
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

        public CaptionElementAlignment CaptionMenuAlignment
        {
            get => (CaptionElementAlignment)GetValue(CaptionMenuAlignmentProperty);
            set
            {
                SetValue(CaptionMenuAlignmentProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CaptionMenuAlignment)));
            }
        }

        public CaptionElementAlignment CaptionTitleAlignment
        {
            get => (CaptionElementAlignment)GetValue(CaptionTitleAlignmentProperty);
            set
            {
                SetValue(CaptionTitleAlignmentProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CaptionTitleAlignment)));
            }
        }

        public CaptionElementAlignment CaptionButtonsAlignment
        {
            get => (CaptionElementAlignment)GetValue(CaptionButtonsAlignmentProperty);
            set
            {
                SetValue(CaptionButtonsAlignmentProperty, value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CaptionButtonsAlignment)));
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
            CaptionMenu = new ObservableCollection<MenuItem>();
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
            {
                CaptionBorder.MouseDown += Border_MouseDown;
                CaptionBorder.MouseMove += CaptionBorder_MouseMove;
            }
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
                DragMove();

            if (e.ClickCount == 2 && CanMaximize)
                ToggleMaximizedState();
        }

        private void CaptionBorder_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && WindowState == WindowState.Maximized)
            {
                Top = e.MouseDevice.GetPosition(e.Device.Target).Y;

                ToggleMaximizedState();
                DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) => OnMinimizeButtonClicked(sender, e);
        private void MaximizeButton_Click(object sender, RoutedEventArgs e) => OnMaximizeButtonClicked(sender, e);
        private void ShadeButton_Click(object sender, RoutedEventArgs e) => OnShadeButtonClicked(sender, e);
        private void CloseButton_Click(object sender, RoutedEventArgs e) => OnCloseButtonClicked(sender, e);

        private void ToggleMaximizedState()
        {
            if (WindowState == WindowState.Maximized)
            {
                CanShade = true;
                WindowState = WindowState.Normal;
            }
            else
            {
                CanShade = false;
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
