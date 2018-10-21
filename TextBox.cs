using System.Windows;
using System.Windows.Controls;

namespace Atlas.UI
{
    public class TextBox : System.Windows.Controls.TextBox
    {
        public static readonly DependencyProperty ShowClearButtonProperty = DependencyProperty.Register(nameof(ShowClearButton), typeof(bool), typeof(TextBox));
        public static readonly DependencyProperty ShowPlaceholderProperty = DependencyProperty.Register(nameof(ShowPlaceholder), typeof(bool), typeof(TextBox));
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(TextBox));
        public static readonly DependencyProperty HasTextProperty = DependencyProperty.Register(nameof(HasText), typeof(bool), typeof(TextBox));

        public bool ShowClearButton
        {
            get => (bool)GetValue(ShowClearButtonProperty);
            set => SetValue(ShowClearButtonProperty, value);
        }

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

        private System.Windows.Controls.Button ClearButton { get; set; }
            
        static TextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ClearButton = GetTemplateChild("PART_ClearButton") as System.Windows.Controls.Button;

            if(ClearButton != null)
                ClearButton.Click += ClearButton_Click;
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            if (Text.Length > 0)
                HasText = true;
            else
                HasText = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
    }
}
