using Atlas.UI.Extensions;
using System.Windows;
using System.Windows.Media;

namespace Atlas.UI
{
    public class Toggle : System.Windows.Controls.CheckBox
    {
        public static DependencyProperty ToggledStateTextProperty = Dependency.Register<string>(nameof(ToggledStateText));
        public static DependencyProperty UntoggledStateTextProperty = Dependency.Register<string>(nameof(UntoggledStateText));
        public static DependencyProperty ToggledStateBrushProperty = Dependency.Register<Brush>(nameof(ToggledStateBrush));
        public static DependencyProperty UntoggledStateBrushProperty = Dependency.Register<Brush>(nameof(UntoggledStateBrush));

        public string ToggledStateText
        {
            get => (string)GetValue(ToggledStateTextProperty);
            set => SetValue(ToggledStateTextProperty, value);
        }

        public string UntoggledStateText
        {
            get => (string)GetValue(UntoggledStateTextProperty);
            set => SetValue(UntoggledStateTextProperty, value);
        }

        public Brush ToggledStateBrush
        {
            get => (Brush)GetValue(ToggledStateBrushProperty);
            set => SetValue(ToggledStateBrushProperty, value);
        }

        public Brush UntoggledStateBrush
        {
            get => (Brush)GetValue(UntoggledStateBrushProperty);
            set => SetValue(UntoggledStateBrushProperty, value);
        }

        static Toggle()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Toggle), new FrameworkPropertyMetadata(typeof(Toggle)));
        }


    }
}
