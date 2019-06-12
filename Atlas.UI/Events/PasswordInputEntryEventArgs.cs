using System;

namespace Atlas.UI.Events
{
    public class PasswordInputEntryEventArgs : EventArgs
    {
        public char[] Password { get; }
        public bool ClearPassword { get; set; } = true;

        public PasswordInputEntryEventArgs(char[] password)
        {
            Password = password;
        }
    }
}
