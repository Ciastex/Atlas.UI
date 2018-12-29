using System.Windows;

namespace Atlas.UI
{
    public class RadioButton : System.Windows.Controls.RadioButton
    {
        static RadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadioButton), new FrameworkPropertyMetadata(typeof(RadioButton)));
        }
    }
}
