using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Atlas.UI.Events;

namespace Atlas.UI
{
    public class TabControl : System.Windows.Controls.TabControl
    {
        private TabPanel TabPanel { get; set; }

        private System.Windows.Controls.Button AddTabButton { get; set; }
        private System.Windows.Controls.Button ScrollLeftButton { get; set; }
        private System.Windows.Controls.Button ScrollRightButton { get; set; }
        private ScrollViewer ScrollView { get; set; }

        public event EventHandler<TabAddButtonClickEventArgs> AddTabButtonPressed;
        public event EventHandler<TabCreatedEventArgs> TabCreated;
        public event EventHandler<TabCloseEventArgs> BeforeTabClosed;
        public event EventHandler TabClosed;

        public event EventHandler TabPanelDoubleClick;

        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register("PlaceholderText", typeof(string), typeof(TabControl));
        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public static readonly DependencyProperty ShowAddButtonProperty = DependencyProperty.Register("ShowAddButton", typeof(bool), typeof(TabControl), new PropertyMetadata(false));
        public bool ShowAddButton
        {
            get { return (bool)GetValue(ShowAddButtonProperty); }
            set { SetValue(ShowAddButtonProperty, value); }
        }

        public static readonly DependencyProperty ShowTabMenuProperty = DependencyProperty.Register("ShowTabMenu", typeof(bool), typeof(TabControl), new PropertyMetadata(false));
        public bool ShowTabMenu
        {
            get { return (bool)GetValue(ShowTabMenuProperty); }
            set { SetValue(ShowTabMenuProperty, value); }
        }

        static TabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TabControl), new FrameworkPropertyMetadata(typeof(TabControl)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            AddTabButton = GetTemplateChild("PART_AddTabButton") as System.Windows.Controls.Button;
            TabPanel = GetTemplateChild("PART_TabPanel") as TabPanel;

            ScrollLeftButton = GetTemplateChild("PART_ScrollLeft") as System.Windows.Controls.Button;
            ScrollRightButton = GetTemplateChild("PART_ScrollRight") as System.Windows.Controls.Button;
            ScrollView = GetTemplateChild("PART_Scroller") as ScrollViewer;

            if (AddTabButton != null)
                AddTabButton.Click += AddTabButton_Click;

            if (TabPanel != null)
                TabPanel.MouseLeftButtonDown += TabPanel_MouseLeftButtonDown;

            if (ScrollLeftButton != null)
                ScrollLeftButton.Click += ScrollLeftButton_Click;

            if (ScrollRightButton != null)
                ScrollRightButton.Click += ScrollRightButton_Click;
        }

        private void AddTabButton_Click(object sender, RoutedEventArgs e)
        {
            AddTabButtonPressed?.Invoke(this, new TabAddButtonClickEventArgs());

            if (e.Handled) return;

            var createdTabItem = new TabItem { Header = "New Tab" };
            Items.Add(createdTabItem);
            ScrollView.ScrollToRightEnd();

            SelectedItem = createdTabItem;

            TabCreated?.Invoke(this, new TabCreatedEventArgs(createdTabItem));
        }

        private void ScrollRightButton_Click(object sender, RoutedEventArgs e)
        {
            ScrollView.ScrollToHorizontalOffset(ScrollView.HorizontalOffset + 25);
        }

        private void ScrollLeftButton_Click(object sender, RoutedEventArgs e)
        {
            ScrollView.ScrollToHorizontalOffset(ScrollView.HorizontalOffset - 25);
        }

        private void TabPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                TabPanelDoubleClick?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveTabItem(TabItem tabItem)
        {
            var beforeClosedArgs = new TabCloseEventArgs();
            BeforeTabClosed?.Invoke(this, beforeClosedArgs);

            if (!beforeClosedArgs.Cancel)
            {
                Items.Remove(tabItem);
                TabClosed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
