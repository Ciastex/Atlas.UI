using System.Windows;

namespace Atlas.UI
{
    public class ProgressBar : System.Windows.Controls.ProgressBar
    {
        static ProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressBar), new FrameworkPropertyMetadata(typeof(ProgressBar)));
        }
    }
}
