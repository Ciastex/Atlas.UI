using Atlas.UI.Extensions;
using System;
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

        public static DependencyProperty IsTopMostProperty = Dependency.Register<bool>(nameof(IsTopMost));

        public bool IsTopMost
        {
            get => (bool)GetValue(IsTopMostProperty);
            set => SetValue(IsTopMostProperty, value);
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
                Child.AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(OnChildPreviewMouseLeftButtonDown), true);

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

        private void OnParentWindowActivated(object sender, EventArgs e)
        {
            SetTopmostState(true);
        }

        private void OnParentWindowDeactivated(object sender, EventArgs e)
        {
            if (IsTopMost == false)
                SetTopmostState(IsTopMost);
        }

        private void OnChildPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetTopmostState(true);

            if (!_parentWindow.IsActive && IsTopMost == false)
                _parentWindow.Activate();
        }

        private static void OnIsTopmostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var popUp = (ParentedPopup)obj;
            popUp.SetTopmostState(popUp.IsTopMost);
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

            if (!WinAPI.GetWindowRect(hwnd, out WinAPI.Rect rect))
                return;

            if (isTop)
                WinAPI.SetWindowPos(hwnd, WinAPI.HWND_TOPMOST, rect.Left, rect.Top, (int)Width, (int)Height, WinAPI.TOPMOST_FLAGS);

            else
            {
                WinAPI.SetWindowPos(hwnd, WinAPI.HWND_BOTTOM, rect.Left, rect.Top, (int)Width, (int)Height, WinAPI.TOPMOST_FLAGS);
                WinAPI.SetWindowPos(hwnd, WinAPI.HWND_TOP, rect.Left, rect.Top, (int)Width, (int)Height, WinAPI.TOPMOST_FLAGS);
                WinAPI.SetWindowPos(hwnd, WinAPI.HWND_NOTOPMOST, rect.Left, rect.Top, (int)Width, (int)Height, WinAPI.TOPMOST_FLAGS);
            }

            _appliedTopMost = isTop;
        }
    }
}
