using Atlas.UI.Extensions;
using System.Windows;

namespace Atlas.UI
{
    public class GroupBox : System.Windows.Controls.GroupBox
    {
        public static DependencyProperty ShowHeaderProperty = Dependency.Register<bool>(nameof(ShowHeader));
        public static DependencyProperty CornerRadiusProperty = Dependency.Register<Thickness>(nameof(CornerRadius));

        public bool ShowHeader
        {
            get => (bool)GetValue(ShowHeaderProperty);
            set => SetValue(ShowHeaderProperty, value);
        }

        public Thickness CornerRadius
        {
            get => (Thickness)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        static GroupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupBox), new FrameworkPropertyMetadata(typeof(GroupBox)));
        }
    }
}
