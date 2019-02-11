using Atlas.UI.Systems;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Atlas.UI
{
    public abstract class SingleInstanceWindow : Window
    {
        private Type GetCallingType()
        {
            var frame = new StackTrace().GetFrame(2).GetMethod().DeclaringType;
            return frame;
        }

        public new void Show()
        {
            if (!GetCallingType().IsAssignableFrom(typeof(SingleInstanceWindowManager)))
                throw new InvalidOperationException("Use SingleInstanceWindowManager to open this type of window.");

            base.Show();
        }

        public new bool? ShowDialog()
        {
            if (!GetCallingType().IsAssignableFrom(typeof(SingleInstanceWindowManager)))
                throw new InvalidOperationException("Use SingleInstanceWindowManager to open this type of window.");

            return base.ShowDialog();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (e.Cancel) return;

            SingleInstanceWindowManager.DeleteWindow(this);
            base.OnClosing(e);
        }
    }
}
