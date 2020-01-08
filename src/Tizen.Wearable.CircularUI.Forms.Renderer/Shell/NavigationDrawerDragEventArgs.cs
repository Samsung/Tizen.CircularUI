using System;
using ElmSharp;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class NavigationDrawerDragEventArgs : EventArgs
    {
        public GestureLayer.GestureState State { get; private set; }
        public GestureLayer.MomentumData Data { get; private set; }

        public NavigationDrawerDragEventArgs(GestureLayer.GestureState state, GestureLayer.MomentumData data)
        {
            State = state;
            Data = data;
        }
    }
}