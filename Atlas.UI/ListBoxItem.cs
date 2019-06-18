using Atlas.UI.Extensions;
using System.Windows;
using System.Windows.Media;

namespace Atlas.UI
{
    public class ListBoxItem : System.Windows.Controls.ListBoxItem
    {
        public static readonly DependencyProperty HoverBackgroundBrushProperty 
            = Dependency.Register<Brush>(nameof(HoverBackgroundBrush));

        public static readonly DependencyProperty SelectionBackgroundBrushProperty
            = Dependency.Register<Brush>(nameof(SelectionBackgroundBrush));

        public Brush HoverBackgroundBrush
        {
            get => (Brush)GetValue(HoverBackgroundBrushProperty);
            set => SetValue(HoverBackgroundBrushProperty, value);
        }

        public Brush SelectionBackgroundBrush
        {
            get => (Brush)GetValue(SelectionBackgroundBrushProperty);
            set => SetValue(SelectionBackgroundBrushProperty, value);
        }

        static ListBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxItem), new FrameworkPropertyMetadata(typeof(ListBoxItem)));
        }
    }
}
