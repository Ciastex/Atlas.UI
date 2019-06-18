using Atlas.UI.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        public static readonly DependencyProperty CopyHeaderCommandProperty = Dependency.Register<ICommand>(nameof(CopyHeaderCommand));
        public static readonly DependencyProperty CopyContentCommandProperty = Dependency.Register<ICommand>(nameof(CopyContentCommand));

        public ICommand CopyHeaderCommand
        {
            get => (ICommand)GetValue(CopyHeaderCommandProperty);
            set => SetValue(CopyHeaderCommandProperty, value);
        }

        public ICommand CopyContentCommand
        {
            get => (ICommand)GetValue(CopyContentCommandProperty);
            set => SetValue(CopyContentCommandProperty, value);
        }

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

        public TableViewItem()
        {
            CopyHeaderCommand = new RelayCommand((obj) =>
            {
                Clipboard.SetText((string)obj);
            });

            CopyContentCommand = new RelayCommand((obj) =>
            {
                Clipboard.SetText((string)obj);
            });
        }
    }
}
