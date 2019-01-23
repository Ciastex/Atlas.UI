using System.Windows;

namespace Atlas.UI
{
    public class ListViewItem : System.Windows.Controls.ListViewItem
    {
        static ListViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListViewItem), new FrameworkPropertyMetadata(typeof(ListViewItem)));
        }
    }
}
