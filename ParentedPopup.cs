using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;

namespace Atlas.UI
{
    public class ParentedPopup : Popup
    {
        private bool? _appliedTopMost;
        private bool _alreadyLoaded;
        private System.Windows.Window _parentWindow;

        public static DependencyProperty IsTopMostProperty = DependencyProperty.Register(nameof(IsTopMost), typeof(bool), typeof(ParentedPopup));

        public bool IsTopMost
        {
            get => (bool)GetValue(IsTopMostProperty);
            set
            {
                SetValue(IsTopMostProperty, value);
            }
        }

        public ParentedPopup()
        {
            Loaded += OnPopupLoaded;
            Unloaded += OnPopupUnloaded;
        }


        void OnPopupLoaded(object sender, RoutedEventArgs e)
        {
            if (_alreadyLoaded)
                return;

            _alreadyLoaded = true;

            if (Child != null)
            {
                Child.AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(OnChildPreviewMouseLeftButtonDown), true);
            }

            _parentWindow = System.Windows.Window.GetWindow(this);

            if (_parentWindow == null)
                return;

            _parentWindow.Activated += OnParentWindowActivated;
            _parentWindow.Deactivated += OnParentWindowDeactivated;
        }

        private void OnPopupUnloaded(object sender, RoutedEventArgs e)
        {
            if (_parentWindow == null)
                return;

            _parentWindow.Activated -= OnParentWindowActivated;
            _parentWindow.Deactivated -= OnParentWindowDeactivated;
        }

        void OnParentWindowActivated(object sender, EventArgs e)
        {
            SetTopmostState(true);
        }

        void OnParentWindowDeactivated(object sender, EventArgs e)
        {
            if (IsTopMost == false)
                SetTopmostState(IsTopMost);
        }

        void OnChildPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetTopmostState(true);

            if (!_parentWindow.IsActive && IsTopMost == false)
                _parentWindow.Activate();
        }

        private static void OnIsTopmostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var thisobj = (ParentedPopup)obj;

            thisobj.SetTopmostState(thisobj.IsTopMost);
        }

        protected override void OnOpened(EventArgs e)
        {
            SetTopmostState(IsTopMost);
            base.OnOpened(e);
        }

        private void SetTopmostState(bool isTop)
        {
            if (_appliedTopMost.HasValue && _appliedTopMost == isTop)
                return;

            if (Child == null)
                return;


            if (!((PresentationSource.FromVisual(Child)) is HwndSource hwndSource))
                return;

            var hwnd = hwndSource.Handle;


            if (!GetWindowRect(hwnd, out Rect rect))
                return;

            if (isTop)
            {
                SetWindowPos(hwnd, HWND_TOPMOST, rect.Left, rect.Top, (int)Width, (int)Height, TOPMOST_FLAGS);
            }
            else
            {
                SetWindowPos(hwnd, HWND_BOTTOM, rect.Left, rect.Top, (int)Width, (int)Height, TOPMOST_FLAGS);
                SetWindowPos(hwnd, HWND_TOP, rect.Left, rect.Top, (int)Width, (int)Height, TOPMOST_FLAGS);
                SetWindowPos(hwnd, HWND_NOTOPMOST, rect.Left, rect.Top, (int)Width, (int)Height, TOPMOST_FLAGS);
            }

            _appliedTopMost = isTop;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect

        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hwnd, out Rect rect);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint flags);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        private const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOMOVE = 0x0002;
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_NOREDRAW = 0x0008;
        const uint SWP_NOACTIVATE = 0x0010;

        const uint SWP_FRAMECHANGED = 0x0020;
        const uint SWP_SHOWWINDOW = 0x0040;
        const uint SWP_HIDEWINDOW = 0x0080;
        const uint SWP_NOCOPYBITS = 0x0100;
        const uint SWP_NOOWNERZORDER = 0x0200;
        const uint SWP_NOSENDCHANGING = 0x0400;

        const uint TOPMOST_FLAGS = SWP_NOACTIVATE | SWP_NOOWNERZORDER | SWP_NOSIZE | SWP_NOMOVE | SWP_NOREDRAW | SWP_NOSENDCHANGING;
    }
}
