using Atlas.UI.Enums;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Atlas.UI.Converters
{
    internal class CaptionElementAlignmentToHorizontalAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsAssignableFrom(typeof(HorizontalAlignment)))
                throw new ArgumentException($"Cannot convert from type '{targetType.Name}'.");

            switch ((HorizontalAlignment)value)
            {
                case HorizontalAlignment.Left:
                    return HorizontalAlignment.Left;
                case HorizontalAlignment.Right:
                    return HorizontalAlignment.Right;
                case HorizontalAlignment.Center:
                    return HorizontalAlignment.Center;
                default: throw new ArgumentException("Converter accepts only CaptionElementAlignment-supported values.");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsAssignableFrom(typeof(CaptionElementAlignment)))
                throw new ArgumentException($"Cannot convert from type '{targetType.Name}'.");

            switch ((CaptionElementAlignment)value)
            {
                case CaptionElementAlignment.Left:
                    return HorizontalAlignment.Left;
                case CaptionElementAlignment.Right:
                    return HorizontalAlignment.Right;
                case CaptionElementAlignment.Center:
                    return HorizontalAlignment.Center;
                default: throw new ArgumentException("Converter accepts only CaptionElementAlignment values.");
            }
        }
    }
}
