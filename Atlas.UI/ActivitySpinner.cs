using Atlas.UI.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace Atlas.UI
{
    public class ActivitySpinner : Control
    {
        public static readonly DependencyProperty IsTaskRunningProperty = Dependency.Register<bool>(nameof(IsTaskRunning));

        public bool IsTaskRunning
        {
            get => (bool)GetValue(IsTaskRunningProperty);
            set => SetValue(IsTaskRunningProperty, value);
        }

        static ActivitySpinner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ActivitySpinner), new FrameworkPropertyMetadata(typeof(ActivitySpinner), null));
        }
    }
}
