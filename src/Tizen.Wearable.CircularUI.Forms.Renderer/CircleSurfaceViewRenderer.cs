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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ElmSharp;
using ElmSharp.Wearable;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;
using ELayout = ElmSharp.Layout;

[assembly: ExportRenderer(typeof(CircleSurfaceView), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CircleSurfaceViewRenderer))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CircleSurfaceViewRenderer : ViewRenderer<CircleSurfaceView, Box>, ICircleSurfaceItemRenderer
    {
        Dictionary<ICircleSurfaceItem, ICircleWidget> _circleSurfaceItems;
        ELayout _surfaceLayout;
        CircleSurface _circleSurface;

        ICircleWidget ICircleSurfaceItemRenderer.GetCircleWidget(ICircleSurfaceItem item)
        {
            if (_circleSurfaceItems.TryGetValue(item, out ICircleWidget widget))
            {
                return widget;
            }
            return null;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CircleSurfaceView> e)
        {
            if (Control == null)
            {
                var box = new Box(XForms.NativeParent);
                box.SetLayoutCallback(OnLayout);
                _surfaceLayout = new ELayout(box);
                _circleSurface = new CircleSurface(_surfaceLayout);
                _circleSurfaceItems = new Dictionary<ICircleSurfaceItem, ICircleWidget>();
                box.PackEnd(_surfaceLayout);
                _surfaceLayout.Show();
                SetNativeControl(box);
            }

            if (e.NewElement != null)
            {
                e.NewElement.CircleSurface = _circleSurface;
                var items = e.NewElement.CircleSurfaceItems as ObservableCollection<ICircleSurfaceItem>;
                items.CollectionChanged += OnCircleSurfaceItemsChanged;
                foreach (var item in items)
                {
                    AddCircleSurfaceItem(item);
                }
            }

            if (e.OldElement != null)
            {
                var items = e.OldElement.CircleSurfaceItems as ObservableCollection<ICircleSurfaceItem>;
                foreach (var item in items)
                {
                    RemoveCircleSurfaceItem(item);
                }
                items.CollectionChanged -= OnCircleSurfaceItemsChanged;
            }

            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (Element != null)
            {
                var items = Element.CircleSurfaceItems as ObservableCollection<ICircleSurfaceItem>;
                foreach (var item in items)
                {
                    RemoveCircleSurfaceItem(item);
                }
                items.CollectionChanged -= OnCircleSurfaceItemsChanged;
            }
            base.Dispose(disposing);
        }

        void OnLayout()
        {
            var rect = Control.Geometry;
            Element.Layout(rect.ToDP());
            _surfaceLayout.Geometry = rect;
        }

        void OnCircleSurfaceItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (ICircleSurfaceItem item in e.NewItems)
                    AddCircleSurfaceItem(item);
            }
            if (e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (ICircleSurfaceItem item in e.OldItems)
                    RemoveCircleSurfaceItem(item);
            }
        }

        void AddCircleSurfaceItem(ICircleSurfaceItem item)
        {
            if (item is CircleProgressBarSurfaceItem)
            {
                var widget = new CircleProgressBarSurfaceItemImplements(item as CircleProgressBarSurfaceItem, _surfaceLayout, _circleSurface);
                _circleSurfaceItems[item] = widget;
            }
            else if (item is CircleSliderSurfaceItem)
            {
                var widget = new CircleSliderSurfaceItemImplements(item as CircleSliderSurfaceItem, _surfaceLayout, _circleSurface);
                _circleSurfaceItems[item] = widget;
            }
        }

        void RemoveCircleSurfaceItem(ICircleSurfaceItem item)
        {
            if (_circleSurfaceItems.TryGetValue(item, out var widget))
            {
                EvasObject obj = widget as EvasObject;
                obj?.Unrealize();
                _circleSurfaceItems.Remove(item);
            }
        }
    }
}
