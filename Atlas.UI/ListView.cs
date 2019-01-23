using System.Windows;

namespace Atlas.UI
{
    public class ListView : System.Windows.Controls.ListView
    {
        static ListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListView), new FrameworkPropertyMetadata(typeof(ListView)));
        }
    }
}
