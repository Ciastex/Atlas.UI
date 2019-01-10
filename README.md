### Atlas.UI
Dark and elegant UI toolkit inspired by Visual Studio design for WPF. Tries to extend on the design by chipping in its own features.

### How to use in your project
1. Add a reference to the library.
2. In your `App.xaml` add this:
```
<ResourceDictionary>
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="pack://application:,,,/Atlas.UI;component/Themes/Atlas.xaml" />
    </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
```

The namespace for all controls is `Atlas.UI`. Whenever you want to use an extended control from the toolkit, add this to your namespace declaration: `xmlns:atlas="clr-namespace:Atlas.UI"`.
