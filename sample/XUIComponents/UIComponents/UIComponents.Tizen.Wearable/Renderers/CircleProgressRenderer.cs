using System;
using System.ComponentModel;
using UIComponents.Extensions;
using UIComponents.Tizen.Wearable.Renderers;
using Xamarin.Forms.Platform.Tizen;
using EProgressBar = ElmSharp.ProgressBar;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;

[assembly: ExportRenderer(typeof(CircleProgress), typeof(CircleProgressRenderer))]
namespace UIComponents.Tizen.Wearable.Renderers
{
    public class CircleProgressRenderer : ViewRenderer<CircleProgress, EProgressBar>
    {
        public CircleProgressRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CircleProgress> e)
        {
            if (Control == null)
            {
                var progressBar = new EProgressBar(TForms.NativeParent);
                SetNativeControl(progressBar);
            }
            if (e.NewElement != null)
            {
                UpdateOption();
            }
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CircleProgress.OptionProperty.PropertyName)
            {
                UpdateOption();
            }
            base.OnElementPropertyChanged(sender, e);
        }

        void UpdateOption()
        {
            if (((CircleProgress)Element).Option == ProgressOptions.Large)
            {
                Control.Style = "process";
            }
            else
            {
                Control.Style = "process/popup/small";
            }
            Control.Show();
            Control.PlayPulse();
        }
    }
}
