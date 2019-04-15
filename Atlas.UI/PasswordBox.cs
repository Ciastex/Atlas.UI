using Atlas.UI.Extensions;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Atlas.UI
{
    public class PasswordBox : TextBox
    {
        public static DependencyProperty CharacterMaskProperty = Dependency.Register<char>(nameof(CharacterMask));
        public static DependencyProperty PasswordProperty = Dependency.Register<string>(nameof(Password));

        private readonly List<char> _password;

        public char CharacterMask
        {
            get => (char)GetValue(CharacterMaskProperty);
            set => SetValue(CharacterMaskProperty, value);
        }

        public string Password
        {
            get
            {
                SetValue(PasswordProperty, string.Join("", _password));
                return (string)GetValue(PasswordProperty);
            }
        }

        public PasswordBox()
        {
            _password = new List<char>();
            CharacterMask = '•';

            CommandManager.AddPreviewExecutedHandler(this, OnCommandExecuted);
        }

        static PasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PasswordBox), new FrameworkPropertyMetadata(typeof(TextBox)));
        }

        public void ClearAll()
        {
            _password.Clear();
            Clear();
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