using Atlas.UI.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Atlas.UI
{
    public class PasswordBox : System.Windows.Controls.TextBox
    {
        private Timer _capsCheckTimer;
        private readonly List<char> _password;

        public static readonly DependencyProperty CharacterMaskProperty = Dependency.Register<char>(nameof(CharacterMask));
        public static readonly DependencyProperty IsCapsLockOnProperty = Dependency.Register<bool>(nameof(IsCapsLockOn));
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

        public char CharacterMask
        {
            get => (char)GetValue(CharacterMaskProperty);
            set => SetValue(CharacterMaskProperty, value);
        }

        public char[] PasswordCharArray => _password.ToArray();

        public bool IsCapsLockOn
        {
            get => (bool)GetValue(IsCapsLockOnProperty);
            set => SetValue(IsCapsLockOnProperty, value);
        }

        public PasswordBox()
        {
            _capsCheckTimer = new Timer(1);
            _capsCheckTimer.Elapsed += CapsCheckTimer_Elapsed;
            _capsCheckTimer.Start();

            _password = new List<char>();

            CharacterMask = '•';

            CommandManager.AddPreviewExecutedHandler(this, OnCommandExecuted);
        }

        private async void CapsCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    if (HasEffectiveKeyboardFocus)
                        IsCapsLockOn = Keyboard.IsKeyToggled(Key.CapsLock);
                });
            }
            catch (TaskCanceledException) { /* don't care */ }
        }

        static PasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBox), new FrameworkPropertyMetadata(typeof(PasswordBox)));
        }

        public new void Clear()
        {
            _password.Clear();
            base.Clear();
        }

        public byte[] ComputeHash(HashAlgorithm algorithm)
        {
            var arr = _password.Select(x => (byte)x).ToArray();
            return algorithm.ComputeHash(arr);
        }

        protected virtual void OnCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Copy)
                e.Handled = true;
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            AddToPassword(e.Text);
            e.Handled = true;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            var pressedKey = e.Key == Key.System ? e.SystemKey : e.Key;
            switch (pressedKey)
            {
                case Key.Space:
                    AddToPassword(" ");
                    e.Handled = true;
                    break;

                case Key.Back:
                case Key.Delete:
                    if (SelectionLength > 0)
                        RemoveFromPassword(SelectionStart, SelectionLength);
                    else if (pressedKey == Key.Delete && CaretIndex < Text.Length)
                        RemoveFromPassword(CaretIndex, 1);
                    else if (pressedKey == Key.Back && CaretIndex > 0)
                    {
                        var caretIndex = CaretIndex;

                        if (CaretIndex > 0 && CaretIndex < Text.Length)
                            caretIndex -= 1;

                        RemoveFromPassword(CaretIndex - 1, 1);
                        CaretIndex = caretIndex;
                    }

                    e.Handled = true;
                    break;

                case Key.Return:
                    if (!AcceptsReturn)
                        e.Handled = true;

                    break;
            }
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            if (Text.Length > 0)
                HasText = true;
            else
                HasText = false;

            base.OnTextChanged(e);
        }

        private void RemoveFromPassword(int startIndex, int length)
        {
            int caretIndex = CaretIndex;
            for (int i = 0; i < length; ++i)
                _password.RemoveAt(startIndex);

            Text = Text.Remove(startIndex, length);
            CaretIndex = caretIndex;
        }

        private void AddToPassword(string text)
        {
            if (SelectionLength > 0)
                RemoveFromPassword(SelectionStart, SelectionLength);

            foreach (var c in text)
            {
                var caretIndex = CaretIndex;
                _password.Insert(caretIndex, c);

                Text = Text.Insert(caretIndex++, CharacterMask.ToString());
                CaretIndex = caretIndex;
            }
        }
    }
}