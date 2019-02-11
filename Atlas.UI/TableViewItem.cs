using Atlas.UI.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Atlas.UI
{
    public class TableViewItem : Control
    {
        public static readonly DependencyProperty HeaderContentProperty = Dependency.Register<string>(nameof(HeaderContent));
        public static readonly DependencyProperty ContentProperty = Dependency.Register<string>(nameof(Content));
        public static readonly DependencyProperty HeaderForegroundProperty = Dependency.Register<Brush>(nameof(HeaderForeground));
        public static readonly DependencyProperty HeaderBackgroundProperty = Dependency.Register<Brush>(nameof(HeaderBackground));
        public static readonly DependencyProperty UniformHeaderWidthProperty = Dependency.Register<double>(nameof(UniformHeaderWidth));

        public double UniformHeaderWidth
        {
            get => (double)GetValue(UniformHeaderWidthProperty);
            set => SetValue(UniformHeaderWidthProperty, value);
        }

        public Brush HeaderForeground
        {
            get => (Brush)GetValue(HeaderForegroundProperty);
            set => SetValue(HeaderForegroundProperty, value);
        }

        public Brush HeaderBackground
        {
            get => (Brush)GetValue(HeaderBackgroundProperty);
            set => SetValue(HeaderForegroundProperty, value);
        }

        public string HeaderContent
        {
            get => (string)GetValue(HeaderContentProperty);
            set => SetValue(HeaderContentProperty, value);
        }

        public string Content
        {
            get => (string)GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }

        static TableViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TableViewItem), new FrameworkPropertyMetadata(typeof(TableViewItem)));
        }
    }
}
