using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Atlas.UI
{
    public class TextBox : System.Windows.Controls.TextBox
    {
        public static readonly DependencyProperty ShowPlaceholderProperty = DependencyProperty.Register(nameof(ShowPlaceholder), typeof(bool), typeof(TextBox));
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(TextBox));
        public static readonly DependencyProperty HasTextProperty = DependencyProperty.Register(nameof(HasText), typeof(bool), typeof(TextBox));
        public static readonly DependencyProperty ScrollToEndOnInputProperty = DependencyProperty.Register(nameof(ScrollToEndOnInput), typeof(bool), typeof(TextBox));

        public bool ShowPlaceholder
        {
            get => (bool)GetValue(ShowPlaceholderProperty);
            set => SetValue(ShowPlaceholderProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public bool HasText
        {
            get => (bool)GetValue(HasTextProperty);
            private set => SetValue(HasTextProperty, value);
        }

        public bool ScrollToEndOnInput
        {
            get => (bool)GetValue(ScrollToEndOnInputProperty);
            set => SetValue(HasTextProperty, value);
        }

        private System.Windows.Controls.Button ClearButton { get; set; }

        static TextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            if (Text.Length > 0)
                HasText = true;
            else
                HasText = false;

            if (e?.Changes?.Any(x => x.AddedLength > 0) == true)
            {
                if (CaretIndex == Text.Length)
                {
                    ScrollToHorizontalOffset(double.PositiveInfinity);
                }
            }
        }
    }
}
