using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Atlas.UI.Systems
{
    public sealed class SingleInstanceWindowManager
    {
        private static ObservableCollection<SingleInstanceWindow> Windows { get; }

        static SingleInstanceWindowManager()
        {
            Windows = new ObservableCollection<SingleInstanceWindow>();
        }

        public static void OpenOrActivate<T>(Window owner = null) where T : SingleInstanceWindow
        {
            if (!IsWindowOpen<T>())
            {
                var instance = Activator.CreateInstance<T>();
                instance.Owner = owner;

                Windows.Add(instance);
                instance.Show();
            }
            else
            {
                var instance = GetOpenWindow<T>();

                if (instance != null)
                    instance.Activate();
            }
        }

        public static bool? OpenOrActivateDialog<T>(Window owner = null) where T : SingleInstanceWindow
        {
            if (!IsWindowOpen<T>())
            {
                var instance = Activator.CreateInstance<T>();
                instance.Owner = null;
                Windows.Add(instance);

                return instance.ShowDialog();
            }
            else throw new InvalidOperationException("This window is already open in dialog mode.");
        }

        public static bool IsWindowOpen<T>() where T : SingleInstanceWindow
        {
            return GetOpenWindow<T>() != null;
        }

        public static T GetOpenWindow<T>() where T : SingleInstanceWindow
        {
            return (T)Windows.FirstOrDefault(window => window.GetType().IsAssignableFrom(typeof(T)));
        }

        internal static void DeleteWindow<T>(T instance) where T : SingleInstanceWindow
        {
            if (Windows.Contains(instance))
                Windows.Remove(instance);
        }
    }
}
