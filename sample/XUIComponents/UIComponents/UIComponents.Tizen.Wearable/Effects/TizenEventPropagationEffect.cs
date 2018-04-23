using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportEffect(typeof(UIComponents.Tizen.Wearable.Effects.TizenEventPropagationEffect), "TizenEventPropagationEffect")]
namespace UIComponents.Tizen.Wearable.Effects
{
    class TizenEventPropagationEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            DoEnable();
        }

        protected override void OnDetached()
        {
            DoEnable(false);
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == UIComponents.Extensions.Effects.TizenEventPropagationEffect.EnablePropagationProperty.PropertyName)
            {
                DoEnable();
            }
        }

        void DoEnable()
        {
            var enablePropagation = UIComponents.Extensions.Effects.TizenEventPropagationEffect.GetEnablePropagation(Element);
            DoEnable(enablePropagation);
        }

        void DoEnable(bool enablePropagation)
        {
            Control.RepeatEvents = enablePropagation;
            Control.PropagateEvents = enablePropagation;
        }
    }
}
