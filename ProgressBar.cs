using System.Windows;

namespace Atlas.UI
{
    public class ProgressBar : System.Windows.Controls.ProgressBar
    {
        public static readonly DependencyProperty ShowProgressTextProperty = DependencyProperty.Register(nameof(ShowProgressText), typeof(bool), typeof(ProgressBar));

        public bool ShowProgressText
        {
            get => (bool)GetValue(ShowProgressTextProperty);
            set => SetValue(ShowProgressTextProperty, value);
        }

        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }
    }
}
