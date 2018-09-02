using System.Windows;

namespace Atlas.UI
{
    public class ListBox : System.Windows.Controls.ListBox
    {

        static ListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBox), new FrameworkPropertyMetadata(typeof(ListBox)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ListBoxItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is ListBoxItem;
        }
    }
}
