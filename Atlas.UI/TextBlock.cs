using Atlas.UI.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace Atlas.UI
{
    public class TextBlock : System.Windows.Controls.TextBlock
    {
        public static readonly DependencyProperty IsBeingTrimmedProperty = Dependency.Register<bool>(nameof(IsBeingTrimmed));
        public static readonly DependencyProperty ShowTooltipOnlyWhenTrimmedProperty = Dependency.Register<bool>(nameof(ShowTooltipOnlyWhenTrimmed));

        public bool IsBeingTrimmed
        {
            get
            {
                Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                SetValue(IsBeingTrimmedProperty, ActualWidth < DesiredSize.Width);

                return (bool)GetValue(IsBeingTrimmedProperty);
            }
        }

        public bool ShowTooltipOnlyWhenTrimmed
        {
            get => (bool)GetValue(ShowTooltipOnlyWhenTrimmedProperty);
            set => SetValue(ShowTooltipOnlyWhenTrimmedProperty, value);
        }

        protected override void OnToolTipOpening(ToolTipEventArgs e)
        {
            if (ShowTooltipOnlyWhenTrimmed && !IsBeingTrimmed)
                e.Handled = true;

            base.OnToolTipOpening(e);
        }
    }
}
