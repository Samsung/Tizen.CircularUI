using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms.Internals;
using ElmSharp;
using ElmSharp.Wearable;
using Xamarin.Forms.Platform.Tizen;

namespace Xamarin.Forms.CircularUI.Renderer
{
    public class CircleListView : Xamarin.Forms.Platform.Tizen.Native.ListView, IRotaryActionWidget
    {
        public CircleListView(EvasObject parent, CircleSurface surface) : base()
        {
            CircleSurface = surface;
            Realize(parent);
        }

        public CircleGenList CircleGenList { get; private set; }

        public IntPtr CircleHandle => CircleGenList.CircleHandle;

        public CircleSurface CircleSurface { get; private set; }

        protected override IntPtr CreateHandle(EvasObject parent)
        {
            CircleGenList = new CircleGenList(parent, CircleSurface);
            RealHandle = CircleGenList.RealHandle;
            return CircleGenList.Handle;
        }
    }
}