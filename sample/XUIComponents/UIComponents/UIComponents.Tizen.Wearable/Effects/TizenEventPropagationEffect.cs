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
        /// <summary>
        /// Attach effect
        /// </summary>
        protected override void OnAttached()
        {
            DoEnable();
        }

        /// <summary>
        /// Detach effect
        /// </summary>
        protected override void OnDetached()
        {
            DoEnable(false);
        }

        /// <summary>
        /// Called when element property is changed.
        /// </summary>
        /// <param name="args">Argument for PropertyChangedEvent</param>
        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == UIComponents.Extensions.Effects.TizenEventPropagationEffect.EnablePropagationProperty.PropertyName)
            {
                DoEnable();
            }
        }

        /// <summary>
        /// Enable Propagation without parameter
        /// </summary>
        void DoEnable()
        {
            var enablePropagation = UIComponents.Extensions.Effects.TizenEventPropagationEffect.GetEnablePropagation(Element);
            DoEnable(enablePropagation);
        }

        /// <summary>
        /// Enable Propagation according to parameter
        /// </summary>
        /// <param name="enablePropagation">Boolean</param>
        void DoEnable(bool enablePropagation)
        {
            Control.RepeatEvents = enablePropagation;
            Control.PropagateEvents = enablePropagation;
        }
    }
}
