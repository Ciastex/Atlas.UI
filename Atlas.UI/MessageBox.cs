using Atlas.UI.Enums;
using Atlas.UI.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Atlas.UI
{
    public class MessageBox : System.Windows.Window
    {
        public static readonly DependencyProperty MessageProperty = Dependency.Register<string>(nameof(Message));
        public static readonly DependencyProperty AdditionalDescriptionProperty = Dependency.Register<string>(nameof(AdditionalDescription));

        private bool WasClosedGracefully { get; set; }
        private Action OnOkClicked { get; set; }
        private Action OnCancelClicked { get; set; }
        private Action OnYesClicked { get; set; }
        private Action OnNoClicked { get; set; }
        private Action OnAbnormalClosure { get; set; }

        private Border CaptionBorder { get; set; }
        private TextBlock MessageTextBlock { get; set; }
        private Button OkButton { get; set; }
        private Button CancelButton { get; set; }
        private Button YesButton { get; set; }
        private Button NoButton { get; set; }

        public MessageBoxButtons ShownButtons { get; private set; } = MessageBoxButtons.Ok;

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public string AdditionalDescription
        {
            get => (string)GetValue(AdditionalDescriptionProperty);
            set => SetValue(AdditionalDescriptionProperty, value);
        }

        static MessageBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBox), new FrameworkPropertyMetadata(typeof(MessageBox)));
        }

        public MessageBox()
        {
            CommandBindings.Add(
                new CommandBinding(ApplicationCommands.Copy, (sender, args) =>
                {
                    Clipboard.SetText(Message);
                })
            );
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            MessageTextBlock?.Focus();
        }

        public override void OnApplyTemplate()
        {
            Closed += MessageBox_Closed;

            OkButton = GetTemplateChild("PART_OkButton") as Button;
            CancelButton = GetTemplateChild("PART_CancelButton") as Button;
            YesButton = GetTemplateChild("PART_YesButton") as Button;
            NoButton = GetTemplateChild("PART_NoButton") as Button;
            CaptionBorder = GetTemplateChild("PART_Caption") as Border;
            MessageTextBlock = GetTemplateChild("PART_Message") as TextBlock;

            if (OkButton != null)
            {
                if (!ShownButtons.HasFlag(MessageBoxButtons.Ok))
                    OkButton.Visibility = Visibility.Collapsed;
                else
                    OkButton.Click += OkButton_Click;
            }

            if (CancelButton != null)
            {
                if (!ShownButtons.HasFlag(MessageBoxButtons.Cancel))
                    CancelButton.Visibility = Visibility.Collapsed;
                else
                    CancelButton.Click += CancelButton_Click;
            }

            if (YesButton != null)
            {
                if (!ShownButtons.HasFlag(MessageBoxButtons.Yes))
                    YesButton.Visibility = Visibility.Collapsed;
                else
                    YesButton.Click += YesButton_Click;
            }

            if (NoButton != null)
            {
                if (!ShownButtons.HasFlag(MessageBoxButtons.No))
                    NoButton.Visibility = Visibility.Collapsed;

                else
                    NoButton.Click += NoButton_Click;
            }

            if (CaptionBorder != null)
                CaptionBorder.MouseDown += CaptionBorder_MouseDown;

            base.OnApplyTemplate();
        }

        public MessageBox Titled(string title)
        {
            Title = title;
            return this;
        }

        public MessageBox CenterOwner()
        {
            if (Owner == null)
                throw new InvalidOperationException("This message box has no owner.");

            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            return this;
        }

        public MessageBox CenterScreen()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            return this;
        }

        public MessageBox PositionedAt(int top, int left)
        {
            WindowStartupLocation = WindowStartupLocation.Manual;

            Top = top;
            Left = left;

            return this;
        }

        public MessageBox WithMessage(string message)
        {
            Message = message;
            return this;
        }

        public MessageBox WithButtons(MessageBoxButtons buttons)
        {
            ShownButtons = buttons;
            return this;
        }

        public MessageBox WithAdditionalDescription(string description)
        {
            AdditionalDescription = description;
            return this;
        }

        public MessageBox OkClickExecutes(Action action)
        {
            OnOkClicked = action;
            return this;
        }

        public MessageBox CancelClickExecutes(Action action)
        {
            OnCancelClicked = action;
            return this;
        }

        public MessageBox YesClickExecutes(Action action)
        {
            OnYesClicked = action;
            return this;
        }

        public MessageBox NoClickExecutes(Action action)
        {
            OnNoClicked = action;
            return this;
        }

        public MessageBox WhenClosedAbnormally(Action action)
        {
            OnAbnormalClosure = action;
            return this;
        }

        public MessageBox OwnedBy(System.Windows.Window owner)
        {
            Owner = owner;

            if (owner.Icon != null)
                Icon = owner.Icon;

            return this;
        }

        public MessageBox WithIcon(ImageSource icon, bool overrideOwnerIcon = false)
        {
            if (Owner != null && !overrideOwnerIcon)
                throw new InvalidOperationException("Cannot override the owner icon when overrideOwnerIcon flag is false.");

            Icon = icon;
            return this;
        }

        public new void Show()
        {
            ShowDialog();
        }

        private void CloseGracefully()
        {
            WasClosedGracefully = true;
            Close();
        }

        private void MessageBox_Closed(object sender, EventArgs e)
        {
            if (!WasClosedGracefully)
                OnAbnormalClosure?.Invoke();

            OnAbnormalClosure = null;
            OnCancelClicked = null;
            OnOkClicked = null;
            OnNoClicked = null;
            OnYesClicked = null;

            if (OkButton != null)
                OkButton.Click -= OkButton_Click;

            if (CancelButton != null)
                CancelButton.Click -= CancelButton_Click;

            if (YesButton != null)
                YesButton.Click -= YesButton_Click;

            if (NoButton != null)
                NoButton.Click -= NoButton_Click;

            if (Owner != null)
                Owner.Activate();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            OnNoClicked?.Invoke();
            CloseGracefully();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            OnYesClicked?.Invoke();
            CloseGracefully();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            OnCancelClicked?.Invoke();
            CloseGracefully();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            OnOkClicked?.Invoke();
            CloseGracefully();
        }

        private void CaptionBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
