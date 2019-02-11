using Atlas.UI.Extensions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Atlas.UI
{
    [DefaultProperty(nameof(Items))]
    [ContentProperty(nameof(Items))]
    public class TableView : Control
    {
        public static readonly DependencyProperty ItemsProperty = Dependency.Register<ObservableCollection<TableViewItem>>(nameof(Items));
        public static readonly DependencyProperty ItemTemplateProperty = Dependency.Register<DataTemplate>(nameof(ItemTemplate));
        public static readonly DependencyProperty UniformHeaderWidthProperty = Dependency.Register<double>(nameof(UniformHeaderWidth), true);
        public static readonly DependencyProperty HasItemsProperty = Dependency.Register<bool>(nameof(HasItems));

        public ObservableCollection<TableViewItem> Items
        {
            get => (ObservableCollection<TableViewItem>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public double UniformHeaderWidth
        {
            get => (double)GetValue(UniformHeaderWidthProperty);
            set => SetValue(UniformHeaderWidthProperty, value);
        }

        public bool HasItems => (bool)GetValue(HasItemsProperty);

        static TableView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TableView), new FrameworkPropertyMetadata(typeof(TableView)));
        }

        public TableView()
        {
            Items = new ObservableCollection<TableViewItem>();
            Items.CollectionChanged += (sender, e) => SetValue(HasItemsProperty, Items.Any());
        }

        public static void SetUniformHeaderWidth(UIElement element, double value)
        {
            element.SetValue(UniformHeaderWidthProperty, value);
        }

        public static double GetUniformHeaderWidth(UIElement element)
        {
            return (double)element.GetValue(UniformHeaderWidthProperty);
        }
    }
}
