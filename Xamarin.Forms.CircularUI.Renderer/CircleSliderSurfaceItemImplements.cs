using ElmSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms.CircularUI;
using Xamarin.Forms.Platform.Tizen;

namespace Xamarin.Forms.CircularUI.Renderer
{
    public class CircleSliderSurfaceItemImplements : ElmSharp.Wearable.CircleSlider
    {
        CircleSliderSurfaceItem _item;

        public CircleSliderSurfaceItemImplements(CircleSliderSurfaceItem item, EvasObject parent, ElmSharp.Wearable.CircleSurface surface) : base(parent, surface)
        {
            _item = item;
            item.PropertyChanged += ItemPropertyChanged;
        }

        void ItemPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == CircleSliderSurfaceItem.BackgroundAngleProperty.PropertyName)
            {
                BackgroundAngle = _item.BackgroundAngle;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BackgroundAngleOffsetProperty.PropertyName)
            {
                BackgroundAngleOffset = _item.BackgroundAngleOffset;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BackgroundColorProperty.PropertyName)
            {
                BackgroundColor = _item.BackgroundColor.ToNative();
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BackgroundLineWidthProperty.PropertyName)
            {
                BackgroundLineWidth = _item.BackgroundLineWidth;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BackgroundRadiusProperty.PropertyName)
            {
                BackgroundRadius = _item.BackgroundRadius;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarAngleProperty.PropertyName)
            {
                BarAngle = _item.BarAngle;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarAngleOffsetProperty.PropertyName)
            {
                BarAngleOffset = _item.BarAngleOffset;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarAngleMaximumProperty.PropertyName)
            {
                BarAngleMaximum = _item.BarAngleMaximum;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarAngleMinimumProperty.PropertyName)
            {
                BarAngleMinimum = _item.BarAngleMinimum;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarColorProperty.PropertyName)
            {
                BarColor = _item.BarColor.ToNative();
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarLineWidthProperty.PropertyName)
            {
                BarLineWidth = _item.BarLineWidth;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.BarRadiusProperty.PropertyName)
            {
                BarRadius = _item.BarRadius;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.IsVisibleProperty.PropertyName)
            {
                if (_item.IsVisible) Show();
                else Hide();
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.ValueProperty.PropertyName)
            {
                Value = _item.Value;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.IsEnabledProperty.PropertyName)
            {
                IsEnabled = _item.IsEnabled;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.MinimumProperty.PropertyName)
            {
                Minimum = _item.Minimum;
            }
            else if (args.PropertyName == CircleSliderSurfaceItem.MaximumProperty.PropertyName)
            {
                Maximum = _item.Maximum;
            }
        }
    }
}
