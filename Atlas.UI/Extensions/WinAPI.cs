using System;
using System.Runtime.InteropServices;

namespace Atlas.UI.Extensions
{
    internal static class WinAPI
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct Margins
        {
            internal int Left;
            internal int Right;
            internal int Top;
            internal int Bottom;
        }


        [StructLayout(LayoutKind.Sequential)]
        internal struct Rect
        {
            internal int Left;
            internal int Top;
            internal int Right;
            internal int Bottom;
        }

        internal static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        internal static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        internal static readonly IntPtr HWND_TOP = new IntPtr(0);
        internal static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        internal const uint SWP_NOSIZE = 0x0001;
        internal const uint SWP_NOMOVE = 0x0002;
        internal const uint SWP_NOZORDER = 0x0004;
        internal const uint SWP_NOREDRAW = 0x0008;
        internal const uint SWP_NOACTIVATE = 0x0010;
        internal const uint SWP_FRAMECHANGED = 0x0020;
        internal const uint SWP_SHOWWINDOW = 0x0040;
        internal const uint SWP_HIDEWINDOW = 0x0080;
        internal const uint SWP_NOCOPYBITS = 0x0100;
        internal const uint SWP_NOOWNERZORDER = 0x0200;
        internal const uint SWP_NOSENDCHANGING = 0x0400;

        internal const uint GWL_STYLE = 0xFFFFFFF0;
        internal const uint GWL_EXSTYLE = 0xFFFFFFEC;

        internal const uint TOPMOST_FLAGS = SWP_NOACTIVATE | SWP_NOOWNERZORDER | SWP_NOSIZE | SWP_NOMOVE | SWP_NOREDRAW | SWP_NOSENDCHANGING;

        [DllImport("dwmapi.dll", PreserveSig = true)]
        internal static extern int DwmSetWindowAttribute(IntPtr handle, int attributes, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        internal static extern int DwmExtendFrameIntoClientArea(IntPtr handle, ref Margins margins);

        [DllImport("user32.dll")]
        internal static extern uint GetWindowLong(IntPtr handle, uint index);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr handle, uint index, uint newLong);

        [DllImport("user32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr handle);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetActiveWindow(IntPtr handle);

        [DllImport("user32.dll")]
        internal static extern bool BringWindowToTop(IntPtr handle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hwnd, out Rect rect);

        [DllImport("user32.dll")]
        internal static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint flags);
    }
}
