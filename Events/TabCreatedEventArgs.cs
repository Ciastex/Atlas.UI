using System;

namespace Atlas.UI.Events
{
    public class TabCreatedEventArgs : EventArgs
    {
        public TabItem CreatedTab { get; }

        public TabCreatedEventArgs(TabItem createdTab)
        {
            CreatedTab = createdTab;
        }
    }
}
