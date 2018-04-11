/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

namespace CircularUI.Tizen
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
