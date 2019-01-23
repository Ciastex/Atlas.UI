using System.Windows;
using System.Windows.Controls;

namespace Atlas.UI.UserControls
{
    public partial class NumericUpDown : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(int), typeof(NumericUpDown));

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public NumericUpDown()
        {
            InitializeComponent();
        }

        private void NumericDown_Click(object sender, RoutedEventArgs e)
        {
            Value -= 1;
        }

        private void NumericUp_Click(object sender, RoutedEventArgs e)
        {
            Value += 1;
        }
    }
}
