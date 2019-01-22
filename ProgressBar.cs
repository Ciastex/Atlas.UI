using Atlas.UI.Extensions;
using System.Windows;

namespace Atlas.UI
{
    public class ProgressBar : System.Windows.Controls.ProgressBar
    {
        public static readonly DependencyProperty ShowProgressTextProperty = Dependency.Register<bool>(nameof(ShowProgressText));
        public static readonly DependencyProperty ProgressTextTemplateProperty = Dependency.Register<string>(nameof(ProgressTextTemplate));

        public bool ShowProgressText
        {
            get => (bool)GetValue(ShowProgressTextProperty);
            set => SetValue(ShowProgressTextProperty, value);
        }

        public string ProgressTextTemplate
        {
            get => (string)GetValue(ProgressTextTemplateProperty);
            set => SetValue(ProgressTextTemplateProperty, value);
        }

        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }
    }
}
