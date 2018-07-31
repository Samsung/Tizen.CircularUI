using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportEffect(typeof(UIComponents.Tizen.Wearable.Effects.TizenStyleEffect), "TizenStyleEffect")]
namespace UIComponents.Tizen.Wearable.Effects
{
    public class TizenStyleEffect : PlatformEffect
    {
        string oldStyle;

        /// <summary>
        /// Attach effect
        /// </summary>
        protected override void OnAttached()
        {
            DoSetStyle();
        }

        /// <summary>
        /// Detach effect
        /// </summary>
        protected override void OnDetached()
        {
            var view = Control as ElmSharp.Widget;
            if (view != null)
            {
                view.Style = oldStyle;
            }
        }

        /// <summary>
        /// Called when element property is changed.
        /// </summary>
        /// <param name="args">Argument for PropertyChangedEvent</param>
        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
            if (args.PropertyName == UIComponents.Extensions.Effects.TizenStyleEffect.StyleProperty.PropertyName)
            {
                DoSetStyle();
            }
        }

        /// <summary>
        /// Set style
        /// </summary>
        void DoSetStyle()
        {
            var view = Control as ElmSharp.Widget;
            if (view != null)
            {
                var style = UIComponents.Extensions.Effects.TizenStyleEffect.GetStyle(Element);
                oldStyle = view.Style;
                view.Style = style;
            }
        }
    }
}
