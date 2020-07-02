using System;

namespace Tizen.Wearable.CircularUI.Forms
{
    public interface IRotaryService
    {
        event EventHandler<RotaryEventArgs> Rotated;
    }
}
