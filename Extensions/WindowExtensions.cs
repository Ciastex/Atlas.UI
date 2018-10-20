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
            {
                currentWindowLong &= ~maximizeBox;
            }
            else
            {
                currentWindowLong |= maximizeBox;
            }

            WinAPI.SetWindowLong(handle, style, currentWindowLong);
        }

        public static void SetResizing(this Window window, bool enable)
        {
            int style = -16;
            int sizeBox = 0x00040000;

            var handle = new WindowInteropHelper(window).Handle;
            var currentWindowLong = WinAPI.GetWindowLong(handle, style);

            if (!enable)
            {
                currentWindowLong &= ~sizeBox;
            }
            else
            {
                currentWindowLong |= sizeBox;
            }

            WinAPI.SetWindowLong(handle, style, currentWindowLong);
        }
    }
}
