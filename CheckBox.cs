using System.Windows;

namespace Atlas.UI
{
    public class CheckBox : System.Windows.Controls.CheckBox
    {
        static CheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckBox), new FrameworkPropertyMetadata(typeof(CheckBox)));
        }
    }
}
