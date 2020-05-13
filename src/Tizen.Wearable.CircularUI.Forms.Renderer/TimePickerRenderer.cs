using Xamarin.Forms;

using XTimePickerRenderer = Xamarin.Forms.Platform.Tizen.TimePickerRenderer;
using CTimePickerRenderer = Tizen.Wearable.CircularUI.Forms.Renderer.TimePickerRenderer;
using System;

[assembly: ExportRenderer(typeof(TimePicker), typeof(CTimePickerRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class TimePickerRenderer : XTimePickerRenderer
    {
        protected override void OnPickerOpened(object sender, EventArgs args)
        {
            Element.FindBezelController()?.Deactivate();
            base.OnPickerOpened(sender, args);
        }

        protected override void OnPickerClosed(object sender, EventArgs args)
        {
            base.OnPickerClosed(sender, args);
            Element.FindBezelController()?.Activate();
        }
    }
}
