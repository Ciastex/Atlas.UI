using System.Windows;
using System.Windows.Media;

namespace Atlas.UI.Extensions
{
    public static class VisualTreeExtensions
    {
        public static T FindParentOfType<T>(this DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentDepObj = child;
            do
            {
                parentDepObj = VisualTreeHelper.GetParent(parentDepObj);
                if (parentDepObj is T parent) return parent;
            }
            while (parentDepObj != null);
            return null;
        }
    }
}
