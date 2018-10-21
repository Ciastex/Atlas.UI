using System.Windows;

namespace Atlas.UI
{
    public class TextBox : System.Windows.Controls.TextBox
    {
        public static readonly DependencyProperty ShowClearButtonProperty = DependencyProperty.Register(nameof(ShowClearButton), typeof(bool), typeof(TextBox));
        public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(nameof(Placeholder), typeof(string), typeof(TextBox));
        public static readonly DependencyProperty HasTextProperty = DependencyProperty.Register(nameof(HasText), typeof(bool), typeof(TextBox));

        public bool ShowClearButton
        {
            get => (bool)GetValue(ShowClearButtonProperty);
            set => SetValue(ShowClearButtonProperty, value);
        }

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public bool HasText
        {
            get
            {
                if (Text.Length > 0)
                    SetValue(HasTextProperty, true);
                else
                    SetValue(HasTextProperty, false);

                return (bool)GetValue(HasTextProperty);
            }
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

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
    }
}
