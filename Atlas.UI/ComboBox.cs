using System.Windows;

namespace Atlas.UI
{
    public class ComboBox : System.Windows.Controls.ComboBox
    {
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(ComboBox));
        public static readonly DependencyProperty UsePlaceholderProperty = DependencyProperty.Register(nameof(UsePlaceholder), typeof(bool), typeof(ComboBox));

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public bool UsePlaceholder
        {
            get => (bool)GetValue(UsePlaceholderProperty);
            set => SetValue(UsePlaceholderProperty, value);
        }

        static ComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ComboBox), new FrameworkPropertyMetadata(typeof(ComboBox)));
        }
    }
}
