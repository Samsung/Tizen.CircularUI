using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(Xamarin.Forms.CircularUI.CircleScrollView), typeof(Xamarin.Forms.CircularUI.Tizen.CircleScrollViewRenderer))]

namespace Xamarin.Forms.CircularUI.Tizen
{
    class CircleScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ScrollView> e)
        {
            if (Control == null)
            {
                var surface = this.GetSurface();
                SetNativeControl(new ElmSharp.Wearable.CircleScroller(Platform.Tizen.Forms.Context.MainWindow, surface));
            }
            base.OnElementChanged(e);
        }
    }
}
