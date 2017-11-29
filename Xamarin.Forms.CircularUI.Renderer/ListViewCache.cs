using System;
using System.Collections.Generic;
using ElmSharp;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.CircularUI.Renderer
{
    public static class ListViewCache
    {
        static readonly Dictionary<Type, CellRenderer> _cellRendererCache = new Dictionary<Type, CellRenderer>();
        static GenItemClass _informalItemClass;
        static GenItemClass _paddingItemClass;

        public static CellRenderer Get(Cell cell, bool IsGroupHeader = false)
        {
            Type type = cell.GetType();
            CellRenderer renderer;
            if (_cellRendererCache.TryGetValue(type, out renderer))
            {
                return renderer;
            }
            else
            {
                if (IsGroupHeader)
                {
                    renderer = new GroupTextCellRenderer();
                }
                else
                {
                    renderer = Registrar.Registered.GetHandler<CellRenderer>(type);
                }
                if (renderer == null)
                {
                    throw new ArgumentNullException("Unsupported cell type");
                }
                return _cellRendererCache[type] = renderer;
            }
        }

        public static GenItemClass InformalItemClass => _informalItemClass ?? (_informalItemClass = new HeaderOrFooterItemClass());
        public static GenItemClass PaddingItemClass => _paddingItemClass ?? (_paddingItemClass = new PaddingItemClass());
    }

    public class HeaderOrFooterItemClass : GenItemClass
    {
        static int ToPx(double dp)
        {
            return (int)Math.Round(dp * Device.Info.ScalingFactor);
        }
        public HeaderOrFooterItemClass() : base("full")
        {
            GetContentHandler = OnGetContent;
        }

        protected EvasObject OnGetContent(object data, string part)
        {
            var ctx = data as TypedItemContext;
            if (ctx != null)
            {
                var element = ctx.Element;
                var renderer = Platform.Tizen.Platform.GetOrCreateRenderer(element);
                if (element.MinimumHeightRequest == -1)
                {
                    var request = element.Measure(double.PositiveInfinity, double.PositiveInfinity);
                    renderer.NativeView.MinimumHeight = ToPx(request.Request.Height);
                }
                else
                {
                    renderer.NativeView.MinimumHeight = ToPx(element.MinimumHeightRequest);
                }
                (renderer as LayoutRenderer)?.RegisterOnLayoutUpdated();
                return renderer.NativeView;
            }
            return null;
        }
    }

    public class PaddingItemClass : GenItemClass
    {
        public PaddingItemClass() : base("padding")
        {
        }
    }
}
