using Atlas.UI.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Atlas.UI
{
    public class TextBox : System.Windows.Controls.TextBox
    {
        public static readonly ICommand ClearCommand = new RoutedUICommand("Clear", "Clear", typeof(TextBox));

        public static readonly DependencyProperty ShowPlaceholderProperty = Dependency.Register<bool>(nameof(ShowPlaceholder));
        public static readonly DependencyProperty PlaceholderProperty = Dependency.Register<string>(nameof(Placeholder));
        public static readonly DependencyProperty HasTextProperty = Dependency.Register<bool>(nameof(HasText));
        public static readonly DependencyProperty HorizontalPlaceholderAlignmentProperty = Dependency.Register<HorizontalAlignment>(nameof(HorizontalPlaceholderAlignment));
        public static readonly DependencyProperty VerticalPlaceholderAlignmentProperty = Dependency.Register<VerticalAlignment>(nameof(VerticalPlaceholderAlignment));
        public static readonly DependencyProperty PlaceholderTextAlignmentProperty = Dependency.Register<TextAlignment>(nameof(PlaceholderTextAlignment));
        public static readonly DependencyProperty PlaceholderPaddingProperty = Dependency.Register<Thickness>(nameof(PlaceholderPadding));

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

        public HorizontalAlignment HorizontalPlaceholderAlignment
        {
            get => (HorizontalAlignment)GetValue(HorizontalPlaceholderAlignmentProperty);
            set => SetValue(HorizontalPlaceholderAlignmentProperty, value);
        }

        public VerticalAlignment VerticalPlaceholderAlignment
        {
            get => (VerticalAlignment)GetValue(VerticalPlaceholderAlignmentProperty);
            set => SetValue(VerticalPlaceholderAlignmentProperty, value);
        }

        public TextAlignment PlaceholderTextAlignment
        {
            get => (TextAlignment)GetValue(VerticalPlaceholderAlignmentProperty);
            set => SetValue(VerticalPlaceholderAlignmentProperty, value);
        }

        public Thickness PlaceholderPadding
        {
            get => (Thickness)GetValue(PlaceholderPaddingProperty);
            set => SetValue(PlaceholderPaddingProperty, value);
        }

        static TextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBox), new FrameworkPropertyMetadata(typeof(TextBox)));
        }

        public TextBox()
        {
            CommandBindings.Add(new CommandBinding(ClearCommand,
                (sender, eventArgs) => Clear(),
                (sender, eventArgs) => eventArgs.CanExecute = true
            ));
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (Text.Length > 0)
                HasText = true;
            else
                HasText = false;

            base.OnTextChanged(e);
        }
    }
}