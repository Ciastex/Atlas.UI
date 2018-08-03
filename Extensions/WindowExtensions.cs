using System.Windows.Interop;

namespace Atlas.UI.Extensions
{
    public static class WindowExtensions
    {
        public static void SetMaximization(this Window window, bool enable)
        {
            int style = -16;
            int maximizeBox = 0x10000;

            var handle = new WindowInteropHelper(window).Handle;
            var currentWindowLong = WinAPI.GetWindowLong(handle, style);

            if (!enable)
                WinAPI.SetWindowLong(handle, style, currentWindowLong & ~maximizeBox);
            else
                WinAPI.SetWindowLong(handle, style, currentWindowLong & maximizeBox);
        }
    }
}
