using Atlas.UI.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Atlas.UI
{
    public class ActivitySpinner : Control
    {
        internal const string FadeInVisualStateName = "FadeIn";
        internal const string FadeOutVisualStateName = "FadeOut";

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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var storyboard = FindResource("SpinStoryboard") as Storyboard;
            var path = GetTemplateChild("SpinnerElement") as Path;

            path.BeginStoryboard(storyboard);

            if (IsTaskRunning)
                VisualStateManager.GoToState(this, FadeInVisualStateName, false);
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property.Name == nameof(IsTaskRunning))
            {
                if ((bool)e.NewValue)
                    VisualStateManager.GoToState(this, FadeInVisualStateName, false);
                else
                    VisualStateManager.GoToState(this, FadeOutVisualStateName, false);
            }
            base.OnPropertyChanged(e);
        }
    }
}
