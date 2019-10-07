using Atlas.UI.Extensions;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;

namespace Atlas.UI.Internal
{
    internal class GlowWindow : System.Windows.Window
    {
        private readonly Window _parentWindow;
        private Border _border;

        private int TargetLeft { get; set; }
        private int TargetTop { get; set; }
        private int TargetWidth { get; set; }
        private int TargetHeight { get; set; }

        public static readonly DependencyProperty GlowBrushProperty = Dependency.Register<Brush>(nameof(GlowBrush));

        public Brush GlowBrush
        {
            get
            {
                SetValue(GlowBrushProperty, _parentWindow.GlowEffectBrush);
                return (Brush)GetValue(GlowBrushProperty);
            }
            set => SetValue(GlowBrushProperty, value);
        }

        static GlowWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GlowWindow), new FrameworkPropertyMetadata(typeof(GlowWindow)));
        }

        public GlowWindow(Window parent)
        {
            _parentWindow = parent;
            _parentWindow.LocationChanged += ParentWindow_LocationChanged;
            _parentWindow.StateChanged += ParentWindow_StateChanged;
            _parentWindow.SizeChanged += ParentWindow_SizeChanged;
            _parentWindow.Closing += ParentWindow_Closing;

            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;

            IsHitTestVisible = false;
            Focusable = false;
            IsEnabled = false;
            ShowInTaskbar = false;
        }

        public void MoveBehindParent()
        {
            var handle = new WindowInteropHelper(this).Handle;
            var ownerHandle = new WindowInteropHelper(_parentWindow).Handle;

            WinAPI.SetWindowPos(
                handle,
                ownerHandle,
                TargetLeft,
                TargetTop,
                TargetWidth,
                TargetHeight,
                WinAPI.SWP_NOOWNERZORDER | WinAPI.SWP_NOACTIVATE
            );
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _border = GetTemplateChild("PART_MainBorder") as Border;
        }

        protected override void OnActivated(EventArgs e)
        {
            Reposition();
            Resize();

            MoveBehindParent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // FIXME: Investigate if the SetWindowLong calls make any sense.
            var handle = new WindowInteropHelper(this).Handle;
            WinAPI.SetWindowLong(handle, WinAPI.GWL_STYLE, 0x96000000);
            WinAPI.SetWindowLong(handle, WinAPI.GWL_EXSTYLE, 0x00080080 | 0x00000020);

            Reposition();
            Resize();

            MoveBehindParent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _parentWindow.LocationChanged -= ParentWindow_LocationChanged;
            _parentWindow.StateChanged -= ParentWindow_StateChanged;
            _parentWindow.SizeChanged -= ParentWindow_SizeChanged;
            _parentWindow.Closing -= ParentWindow_Closing;

            base.OnClosing(e);
        }

        private void ParentWindow_LocationChanged(object sender, EventArgs e)
        {
            Reposition();
            MoveBehindParent();
        }

        private void ParentWindow_StateChanged(object sender, EventArgs e)
        {
            if (_parentWindow.WindowState == WindowState.Minimized)
            {
                Height = 0;
                Width = 0;
                Opacity = 0;
            }
            else if (_parentWindow.WindowState == WindowState.Normal)
            {
                Reposition();
                Resize();

                // FIXME: Assumes DWM is on by default and animations are enabled.
                Thread.Sleep(150);

                Opacity = 1;
                WindowState = WindowState.Normal;
                MoveBehindParent();
            }
            else if (_parentWindow.WindowState == WindowState.Maximized)
            {
                Height = 0;
                Width = 0;
                Opacity = 0;
            }
        }

        private void ParentWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Resize();
            MoveBehindParent();
        }

        private void ParentWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                Close();
            }
        }

        private void Reposition()
        {
            TargetLeft = (int)_parentWindow.Left - 10;
            TargetTop = (int)_parentWindow.Top - 10;
        }

        private void Resize()
        {
            TargetWidth = (int)_parentWindow.Width + 20;
            TargetHeight = (int)_parentWindow.Height + 20;
        }
    }
}
