using Atlas.UI.Extensions;
using System.Windows;
using System.Windows.Input;

namespace Atlas.UI
{
    public class TabItem : System.Windows.Controls.TabItem
    {
        private System.Windows.Controls.Button CloseButton { get; set; }

        public static readonly DependencyProperty CanCloseProperty = Dependency.Register<bool>(nameof(CanClose));

        public bool CanClose
        {
            get { return (bool)GetValue(CanCloseProperty); }
            set { SetValue(CanCloseProperty, value); }
        }

        static TabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabItem), new FrameworkPropertyMetadata(typeof(TabItem)));
        }

        public TabItem()
        {
            MouseDown += AtlasTabItem_MouseDown;
        }

        ~TabItem()
        {
            MouseDown -= AtlasTabItem_MouseDown;
        }

        private void AtlasTabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed && CanClose)
                Close();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            CloseButton = GetTemplateChild("PART_CloseButton") as System.Windows.Controls.Button;

            if (CloseButton != null)
                CloseButton.Click += CloseButton_Click;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void Close()
        {
            var tabControl = Parent as TabControl;
            tabControl?.RemoveTabItem(this);
        }
    }
}
