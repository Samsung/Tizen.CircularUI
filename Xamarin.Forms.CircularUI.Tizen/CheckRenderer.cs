using System;
using System.ComponentModel;
using Xamarin.Forms.CircularUI;
using Xamarin.Forms.CircularUI.Tizen;
using Xamarin.Forms.Platform.Tizen;
using ECheck = ElmSharp.Check;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;

[assembly: ExportRenderer(typeof(Check), typeof(CheckRenderer))]

namespace Xamarin.Forms.CircularUI.Tizen
{
    internal static class CheckStyle
    {
        public const string Default = "default";
        public const string Onoff = "on&off";
        public const string Small = "small";
    }

    public class CheckRenderer : SwitchRenderer
    {
        public CheckRenderer()
        {
            RegisterPropertyHandler(Check.DisplayStyleProperty, UpdateStyle);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            if (Control == null)
            {
                SetNativeControl(new ECheck(TForms.NativeParent)
                {
                    PropagateEvents = false,
                    Style = CheckStyle.Default
                });
                Control.StateChanged += OnStateChanged;
            }
            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Control != null)
                {
                    Control.StateChanged -= OnStateChanged;
                }
            }
            base.Dispose(disposing);
        }

        void UpdateStyle()
        {
            var style = ((Check)Element).DisplayStyle;
            switch (style)
            {
                case CheckDisplayStyle.Onoff:
                    Control.Style = CheckStyle.Onoff;
                    break;
                case CheckDisplayStyle.Small:
                    Control.Style = CheckStyle.Small;
                    break;
                case CheckDisplayStyle.Default:
                default:
                    Control.Style = CheckStyle.Default;
                    break;
            }
            ((IVisualElementController)Element).NativeSizeChanged();
        }

        void OnStateChanged(object sender, EventArgs e)
        {
            Element.SetValue(Switch.IsToggledProperty, Control.IsChecked);
        }
    }
}
