using System.Windows;

namespace Atlas.UI
{
    public class ListBoxItem : System.Windows.Controls.ListBoxItem
    {
        public static readonly DependencyProperty ShowRemoveButtonProperty = DependencyProperty.Register("ShowRemoveButton", typeof(bool), typeof(ListBoxItem));
        public bool ShowRemoveButton
        {
            get { return (bool)GetValue(ShowRemoveButtonProperty); }
            set { SetValue(ShowRemoveButtonProperty, value); }
        }

        public static readonly DependencyProperty ShowAddItemButtonProperty = DependencyProperty.Register("ShowAddItemButton", typeof(bool), typeof(ListBoxItem));
        public bool ShowAddItemButton
        {
            get { return (bool)GetValue(ShowAddItemButtonProperty); }
            set { SetValue(ShowRemoveButtonProperty, value); }
        }

        static ListBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxItem), new FrameworkPropertyMetadata(typeof(ListBoxItem)));
        }
    }
}
