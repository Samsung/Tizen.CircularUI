using ElmSharp;
using ElmSharp.Wearable;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class WatchListView : Xamarin.Forms.Platform.Tizen.Native.Watch.WatchListView
    {
        GenItemClass _cancelEffectTemplete;

        public WatchListView(EvasObject parent, CircleSurface surface) : base(parent, surface)
        {
        }

        public override void SetHeader(VisualElement header)
        {
            base.SetHeader(header);
            if (header != null && CircleListView.GetCancelEffect(header))
            {
                FirstItem.UpdateItemClass(GetCancelEffectTemplete(), HeaderItemContext);
            }
        }

        public override void SetFooter(VisualElement footer)
        {
            base.SetFooter(footer);
            if (footer != null && CircleListView.GetCancelEffect(footer))
            {
                LastItem.UpdateItemClass(GetCancelEffectTemplete(), FooterItemContext);
            }
        }

        GenItemClass GetCancelEffectTemplete()
        {
            if (_cancelEffectTemplete != null)
                return _cancelEffectTemplete;
            _cancelEffectTemplete = new GenItemClass("full_off")
            {
                GetContentHandler = (data, part) =>
                {
                    var context = data as HeaderFooterItemContext;
                    if (context == null || context.Element == null)
                        return null;

                    var renderer = Platform.GetOrCreateRenderer(context.Element);
                    if (context.Element.MinimumHeightRequest == -1)
                    {
                        SizeRequest request = context.Element.Measure(double.PositiveInfinity, double.PositiveInfinity);
                        renderer.NativeView.MinimumHeight = XForms.ConvertToScaledPixel(request.Request.Height);
                    }
                    else
                    {
                        renderer.NativeView.MinimumHeight = XForms.ConvertToScaledPixel(context.Element.MinimumHeightRequest);
                    }

                    (renderer as LayoutRenderer)?.RegisterOnLayoutUpdated();

                    return renderer.NativeView;
                }
            };

            return _cancelEffectTemplete;
        }
    }
}
