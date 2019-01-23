using System;
using System.Globalization;
using System.Windows.Data;

namespace Atlas.UI.Converters
{
    internal class ProgressTextFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return (values[0] as string).Replace("%max%", (values[1] as ProgressBar).Maximum.ToString())
                                        .Replace("%val%", (values[1] as ProgressBar).Value.ToString())
                                        .Replace("%min%", (values[1] as ProgressBar).Minimum.ToString());
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
