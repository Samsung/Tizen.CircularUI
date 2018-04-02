using System;
using WearableUIGallery.Extensions;
using Tizen.Extension.Sample;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using CircularUI.Tizen;
using ElmSharp;

[assembly: ExportCell(typeof(RenderCircleListView), typeof(RenderListViewRenderer))]

namespace Tizen.Extension.Sample
{
    public class RenderListViewRenderer : CircleListViewRenderer
    {
        new RenderCircleListView Element => base.Element as RenderCircleListView;

        protected override void OnElementChanged(ElementChangedEventArgs<CircularUI.CircleListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.Changed -= OnChanged;
                Control.RenderPost -= OnRenderPost;
                Control.ScrollAnimationStarted -= OnScrollStarted;
                Control.ScrollAnimationStopped -= OnScrollStopped;
            }

            if (e.NewElement != null)
            {
                Control.Style = "solid/default";
                Control.AllowFocus(false);
                Control.RenderPost += OnRenderPost;
                Control.Changed += OnChanged;
                Control.ScrollAnimationStarted += OnScrollStarted;
                Control.ScrollAnimationStopped += OnScrollStopped;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Element != null)
                {
                    Control.Changed -= OnChanged;
                    Control.RenderPost -= OnRenderPost;
                    Control.ScrollAnimationStarted -= OnScrollStarted;
                    Control.ScrollAnimationStopped -= OnScrollStopped;
                }
            }
            base.Dispose(disposing);
        }

        private void OnRenderPost(object sender, EventArgs e)
        {
            Element.SendRenderPost();
        }

        private void OnScrollStopped(object sender, EventArgs e)
        {
            Element.SendScrollStopped();
        }

        private void OnScrollStarted(object sender, EventArgs e)
        {
            Element.SendScrollStarted();
        }

        private void OnChanged(object sender, EventArgs e)
        {
            Element.SendChanged();
        }
    }
}
