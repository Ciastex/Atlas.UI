using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Atlas.UI.Enums;
using Atlas.UI.Events;
using Atlas.UI.Extensions;

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

        public static readonly DependencyProperty PlaceholderTextProperty = Dependency.Register<string>(nameof(PlaceholderText));
        public static readonly DependencyProperty ShowAddButtonProperty = Dependency.Register<bool>(nameof(ShowAddButton));
        public static readonly DependencyProperty ShowTabMenuProperty = Dependency.Register<bool>(nameof(ShowTabMenu));
        public static readonly DependencyProperty NewTabBehaviorProperty = Dependency.Register<NewTabBehavior>(nameof(NewTabBehavior));

        public string PlaceholderText
        {
            get => (string)GetValue(PlaceholderTextProperty);
            set => SetValue(PlaceholderTextProperty, value);
        }

        public bool ShowAddButton
        {
            get => (bool)GetValue(ShowAddButtonProperty);
            set => SetValue(ShowAddButtonProperty, value);
        }

        public bool ShowTabMenu
        {
            get => (bool)GetValue(ShowTabMenuProperty);
            set => SetValue(ShowTabMenuProperty, value);
        }

        public NewTabBehavior NewTabBehavior
        {
            get => (NewTabBehavior)GetValue(NewTabBehaviorProperty);
            set => SetValue(NewTabBehaviorProperty, value);
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
            var eventArgs = new TabAddButtonClickEventArgs();
            AddTabButtonPressed?.Invoke(this, eventArgs);

            if (eventArgs.Handled) return;

            var createdTabItem = new TabItem { Header = "New Tab" };

            if (NewTabBehavior.HasFlag(NewTabBehavior.CanClose))
                createdTabItem.CanClose = true;

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
