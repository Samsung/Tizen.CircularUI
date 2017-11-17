using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.Tizen;

namespace Xamarin.Forms.CircularUI.Renderer
{
    static class CircleWidgetRendererExtension
    {
        internal static ElmSharp.Wearable.CircleSurface GetSurface(this IVisualElementRenderer renderer)
        {
            if (null != renderer.Element)
            {
                Element element = renderer.Element;
                while (!(element is CirclePage))
                {
                    element = element.Parent;
                }
                if (element != null)
                {
                    var circlePageRenderer = Xamarin.Forms.Platform.Tizen.Platform.GetRenderer(element) as CirclePageRenderer;
                    if (null != renderer)
                    {
                        return circlePageRenderer.CircleSurface;
                    }
                }
            }

            return null;
        }
    }
}
