using System;
using System.Diagnostics;
using System.Windows;

namespace Atlas.UI.Extensions
{
    public static class Dependency
    {
        public static DependencyProperty Register<T>(string propertyName, bool attached = false, PropertyMetadata defaultMetadata = null)
        {
            var callingType = new StackFrame(1).GetMethod().DeclaringType;

            if (!typeof(DependencyObject).IsAssignableFrom(callingType))
                throw new ArgumentException("The calling type is not a dependency object.");

            if (attached)
                return DependencyProperty.RegisterAttached(propertyName, typeof(T), callingType, defaultMetadata);
            else
                return DependencyProperty.Register(propertyName, typeof(T), callingType, defaultMetadata);
        }

        public static DependencyPropertyKey RegisterReadOnly<T>(string propertyName, bool attached = false, PropertyMetadata defaultMetadata = null)
        {
            var callingType = new StackFrame(1).GetMethod().DeclaringType;

            if (!typeof(DependencyObject).IsAssignableFrom(callingType))
                throw new ArgumentException("The calling type is not a dependency object.");

            if (attached)
                return DependencyProperty.RegisterAttachedReadOnly(propertyName, typeof(T), callingType, defaultMetadata);
            else
                return DependencyProperty.RegisterReadOnly(propertyName, typeof(T), callingType, defaultMetadata);
        }
    }
}
