using System.Windows;

namespace Atlas.UI
{
    public class ListBox : System.Windows.Controls.ListBox
    {
        static ListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBox), new FrameworkPropertyMetadata(typeof(ListBox)));
        }

        protected override DependencyObject GetContainerForItemOverride() => new ListBoxItem();
        protected override bool IsItemItsOwnContainerOverride(object item) => item is ListBoxItem;
    }
}
