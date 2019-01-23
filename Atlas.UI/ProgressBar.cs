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

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            UpdateTemplateConverter();
        }

        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            UpdateTemplateConverter();
        }

        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            UpdateTemplateConverter();
        }

        private void UpdateTemplateConverter()
        {
            // TODO: Find a cleaner way of forcing an update.
            var previous = ProgressTextTemplate;
            ProgressTextTemplate = null;
            ProgressTextTemplate = previous;
        }
    }
}
