using System;

namespace Xamarin.Forms.CircularUI
{
    public delegate void RotaryEventHandler(RotaryEventArgs args);

    public class RotaryEventArgs : EventArgs
    {
        public bool IsClockwise { get; set; }
    }
}