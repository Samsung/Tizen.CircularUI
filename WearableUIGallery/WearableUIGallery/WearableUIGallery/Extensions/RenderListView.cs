using System;
using Xamarin.Forms;
using Xamarin.Forms.CircularUI;

namespace WearableUIGallery.Extensions
{
    public class RenderCircleListView : CircleListView
    {
        public event EventHandler Changed;
        public event EventHandler RenderPost;
        public event EventHandler ScrollStarted;
        public event EventHandler ScrollStopped;

        public void SendChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void SendRenderPost()
        {
            RenderPost?.Invoke(this, EventArgs.Empty);
        }

        public void SendScrollStarted()
        {
            ScrollStarted?.Invoke(this, EventArgs.Empty);
        }

        public void SendScrollStopped()
        {
            ScrollStopped?.Invoke(this, EventArgs.Empty);
        }
    }
}
