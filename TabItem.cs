using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Atlas.UI
{
    public class TabItem : System.Windows.Controls.TabItem
    {
        private System.Windows.Controls.Button CloseButton { get; set; }

        public static readonly DependencyProperty IsClosableProperty = DependencyProperty.Register("IsClosable", typeof(bool), typeof(TabItem), new PropertyMetadata(true));

        public bool IsClosable
        {
            get { return (bool)GetValue(IsClosableProperty); }
            set { SetValue(IsClosableProperty, value); }
        }

        static TabItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabItem), new FrameworkPropertyMetadata(typeof(TabItem)));
        }

        public TabItem()
        {
            MouseMove += AtlasTabItem_MouseMove;
            MouseDown += AtlasTabItem_MouseDown;
            Drop += AtlasTabItem_Drop;
        }

        private void AtlasTabItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed && IsClosable)
            {
                Close();
            }
        }

        private void AtlasTabItem_Drop(object sender, DragEventArgs e)
        {
            var tabItemTarget = e.Source as TabItem;
            var tabItemSource = e.Data.GetData(typeof(TabItem)) as TabItem;

            if (tabItemTarget == null)
                return;

            if (tabItemSource == null)
                return;

            if (!tabItemTarget.Equals(tabItemSource))
            {
                var tabControl = tabItemTarget.Parent as TabControl;

                if (tabControl == null)
                    return;

                var sourceIndex = tabControl.Items.IndexOf(tabItemSource);
                var targetIndex = tabControl.Items.IndexOf(tabItemTarget);

                tabControl.Items.Remove(tabItemSource);
                tabControl.Items.Insert(targetIndex, tabItemSource);

                tabControl.Items.Remove(tabItemTarget);
                tabControl.Items.Insert(sourceIndex, tabItemTarget);

                tabControl.SelectedIndex = targetIndex;
            }
        }

        private void AtlasTabItem_MouseMove(object sender, MouseEventArgs e)
        {
            var tabItem = e.Source as TabItem;
            if (tabItem == null)
                return;

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.All);
            }
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
