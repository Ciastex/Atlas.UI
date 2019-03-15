using System;

namespace Atlas.UI.Enums
{
    [Flags]
    public enum MessageBoxButtons
    {
        Yes = 0x01,
        No = 0x02,
        Ok = 0x04,
        Cancel = 0x08
    }
}
