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
            if (HasHeader() && CircleListView.GetCancelEffect(header))
            {
                FirstItem.UpdateItemClass(GetCancelEffectTemplete(), header);
            }
        }

        public override void SetFooter(VisualElement footer)
        {
            base.SetFooter(footer);
            if (HasFooter() && CircleListView.GetCancelEffect(footer))
            {
                LastItem.UpdateItemClass(GetCancelEffectTemplete(), footer);
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
                    VisualElement element = data as VisualElement;
                    var renderer = Platform.GetOrCreateRenderer(element);

                    if (element.MinimumHeightRequest == -1)
                    {
                        SizeRequest request = element.Measure(double.PositiveInfinity, double.PositiveInfinity);
                        renderer.NativeView.MinimumHeight = XForms.ConvertToScaledPixel(request.Request.Height);
                    }
                    else
                    {
                        renderer.NativeView.MinimumHeight = XForms.ConvertToScaledPixel(element.MinimumHeightRequest);
                    }
                    (renderer as LayoutRenderer)?.RegisterOnLayoutUpdated();

                    return renderer.NativeView;
                }
            };

            return _cancelEffectTemplete;
        }
    }
}
