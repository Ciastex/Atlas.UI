using Atlas.UI.Enums;
using Atlas.UI.Events;
using Atlas.UI.Extensions;
using System;
using System.Globalization;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Atlas.UI
{
    public class IntegerUpDown : Control
    {
        public static DependencyProperty MaximumProperty = Dependency.Register<int>(nameof(Maximum));
        public static DependencyProperty MinimumProperty = Dependency.Register<int>(nameof(Minimum));
        public static DependencyProperty ValueProperty = Dependency.Register<int>(nameof(Value));
        public static DependencyProperty DisallowUserInputProperty = Dependency.Register<bool>(nameof(DisallowUserInput));
        public static DependencyProperty ValueAlignmentProperty = Dependency.Register<TextAlignment>(nameof(ValueAlignment));
        public static DependencyProperty ValueOverflowBehaviorProperty = Dependency.Register<IntegerOverflowBehavior>(nameof(ValueOverflowBehavior));

        public event EventHandler<IntegerUpDownRollOverEventArgs> RolledOver;

        private System.Windows.Controls.Button _incrementButton;
        private System.Windows.Controls.Button _decrementButton;
        private TextBox _inputTextBox;

        private Timer _spinTimer;
        private Timer _buttonHoldTimer;

        private bool _countingUp;

        public int Maximum
        {
            get => (int)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public int Minimum
        {
            get => (int)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public bool DisallowUserInput
        {
            get => (bool)GetValue(DisallowUserInputProperty);
            set => SetValue(DisallowUserInputProperty, value);
        }

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public TextAlignment ValueAlignment
        {
            get => (TextAlignment)GetValue(ValueAlignmentProperty);
            set => SetValue(ValueAlignmentProperty, value);
        }

        public IntegerOverflowBehavior ValueOverflowBehavior
        {
            get => (IntegerOverflowBehavior)GetValue(ValueOverflowBehaviorProperty);
            set => SetValue(ValueOverflowBehaviorProperty, value);
        }

        static IntegerUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(IntegerUpDown), new FrameworkPropertyMetadata(typeof(IntegerUpDown)));
        }

        public IntegerUpDown()
        {
            _spinTimer = new Timer(75);
            _buttonHoldTimer = new Timer(500);

            _spinTimer.Elapsed += _spinTimer_Elapsed;
            _buttonHoldTimer.Elapsed += _buttonHoldTimer_Elapsed;
        }

        public override void OnApplyTemplate()
        {
            _incrementButton = GetTemplateChild("PART_Up") as System.Windows.Controls.Button;
            _decrementButton = GetTemplateChild("PART_Down") as System.Windows.Controls.Button;
            _inputTextBox = GetTemplateChild("PART_InputBox") as TextBox;

            if (_incrementButton != null)
            {
                _incrementButton.PreviewMouseDown += _incrementButton_MouseDown;
                _incrementButton.PreviewMouseUp += _anyButton_MouseUp;
            }

            if (_decrementButton != null)
            {
                _decrementButton.PreviewMouseDown += _decrementButton_MouseDown;
                _decrementButton.PreviewMouseUp += _anyButton_MouseUp;
            }

            if (_inputTextBox != null)
            {
                _inputTextBox.PreviewTextInput += _inputTextBox_PreviewTextInput;
                _inputTextBox.TextChanged += _inputTextBox_TextChanged;
            }

            base.OnApplyTemplate();
        }

        private void _incrementButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;

            StepValue(1);

            _countingUp = true;
            _buttonHoldTimer.Start();
        }

        private void _anyButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;

            _buttonHoldTimer.Stop();
            _spinTimer.Stop();

            _spinTimer.Interval = 75;
        }

        private void _decrementButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left) return;

            StepValue(-1);

            _countingUp = false;
            _buttonHoldTimer.Start();
        }

        private void _inputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(_inputTextBox.Text, out int value))
            {
                if (value > Maximum)
                    _inputTextBox.Text = Maximum.ToString();
                else if (value < Minimum)
                    _inputTextBox.Text = Minimum.ToString();
            }
        }

        private void _inputTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, NumberStyles.AllowLeadingSign & ~NumberStyles.AllowLeadingWhite & ~NumberStyles.AllowTrailingWhite, null, out int parseResult))
                e.Handled = true;
        }

        private void _spinTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() => StepValue(_countingUp ? 1 : -1));
        }

        private void _buttonHoldTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() => _spinTimer.Start());

            if (_spinTimer.Interval - 5 > 1)
                _spinTimer.Interval -= 5;
        }

        public void StepValue(int by)
        {
            if (Value + by > Maximum)
            {
                switch (ValueOverflowBehavior)
                {
                    case IntegerOverflowBehavior.Clamp:
                        Value = Maximum;
                        break;
                    case IntegerOverflowBehavior.RollOver:
                        Value = Minimum;
                        RolledOver?.Invoke(this, new IntegerUpDownRollOverEventArgs(RollOverDirection.Up));
                        break;
                }
            }
            else if (Value + by < Minimum)
            {
                switch (ValueOverflowBehavior)
                {
                    case IntegerOverflowBehavior.Clamp:
                        Value = Minimum;
                        break;
                    case IntegerOverflowBehavior.RollOver:
                        Value = Maximum;
                        RolledOver?.Invoke(this, new IntegerUpDownRollOverEventArgs(RollOverDirection.Down));
                        break;
                }
            }
            else Value += by;
        }
    }
}
