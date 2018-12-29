using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Atlas.UI
{
    public class GroupBox : System.Windows.Controls.GroupBox
    {
        public static DependencyProperty HeaderAlignmentProperty = DependencyProperty.Register(
            nameof(HeaderAlignment), typeof(HorizontalAlignment), typeof(GroupBox));

        public static DependencyProperty ShowHeaderProperty = DependencyProperty.Register(
            nameof(ShowHeader), typeof(bool), typeof(GroupBox));

        public HorizontalAlignment HeaderAlignment
        {
            get => (HorizontalAlignment)GetValue(HeaderAlignmentProperty);
            set => SetValue(HeaderAlignmentProperty, value);
        }

        public bool ShowHeader
        {
            get => (bool)GetValue(ShowHeaderProperty);
            set => SetValue(ShowHeaderProperty, value);
        }

        static GroupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GroupBox), new FrameworkPropertyMetadata(typeof(GroupBox)));
        }
    }
}
