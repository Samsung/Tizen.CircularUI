using Xamarin.Forms;

using XDatePickerRenderer = Xamarin.Forms.Platform.Tizen.DatePickerRenderer;
using CDatePickerRenderer = Tizen.Wearable.CircularUI.Forms.Renderer.DatePickerRenderer;
using System;

[assembly: ExportRenderer(typeof(DatePicker), typeof(CDatePickerRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class DatePickerRenderer : XDatePickerRenderer
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
