using Atlas.UI.Enums;
using System;

namespace Atlas.UI.Events
{
    public class IntegerUpDownRollOverEventArgs : EventArgs
    {
        public RollOverDirection Direction { get; }

        public IntegerUpDownRollOverEventArgs(RollOverDirection direction)
        {
            Direction = direction;
        }
    }
}
