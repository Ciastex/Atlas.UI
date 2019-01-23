using Atlas.UI.WindowStates;

namespace Atlas.UI.Events
{
    public class ShadeStateChangedEventArgs
    {
        public ShadeState ShadeState { get; }

        public ShadeStateChangedEventArgs(ShadeState shadeState)
        {
            ShadeState = ShadeState;
        }
    }
}
