using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.CircularUI
{
    public class CircleSurfaceItem : Element, ICircleSurfaceItem
    {
        public static readonly BindableProperty BackgroundAngleProperty = BindableProperty.Create(nameof(BackgroundAngle), typeof(double), typeof(CircleSurfaceItem), 360.0);
        public static readonly BindableProperty BackgroundAngleOffsetProperty = BindableProperty.Create(nameof(BackgroundAngleOffset), typeof(double), typeof(CircleSurfaceItem), 0.0);
        public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(CircleSurfaceItem), default(Color));
        public static readonly BindableProperty BackgroundLineWidthProperty = BindableProperty.Create(nameof(BackgroundLineWidth), typeof(int), typeof(CircleSurfaceItem), -1);
        public static readonly BindableProperty BackgroundRadiusProperty = BindableProperty.Create(nameof(BackgroundRadius), typeof(double), typeof(CircleSurfaceItem), -1.0);

        public static readonly BindableProperty BarAngleProperty = BindableProperty.Create(nameof(BarAngle), typeof(double), typeof(CircleSurfaceItem), 0.0);
        public static readonly BindableProperty BarAngleOffsetProperty = BindableProperty.Create(nameof(BarAngleOffset), typeof(double), typeof(CircleSurfaceItem), 0.0);
        public static readonly BindableProperty BarAngleMaximumProperty = BindableProperty.Create(nameof(BarAngleMaximum), typeof(double), typeof(CircleSurfaceItem), 360.0);
        public static readonly BindableProperty BarAngleMinimumProperty = BindableProperty.Create(nameof(BarAngleMinimum), typeof(double), typeof(CircleSurfaceItem), 0.0);
        public static readonly BindableProperty BarColorProperty = BindableProperty.Create(nameof(BarColor), typeof(Color), typeof(CircleSurfaceItem), default(Color));
        public static readonly BindableProperty BarLineWidthProperty = BindableProperty.Create(nameof(BarLineWidth), typeof(int), typeof(CircleSurfaceItem), -1);
        public static readonly BindableProperty BarRadiusProperty = BindableProperty.Create(nameof(BarRadius), typeof(double), typeof(CircleSurfaceItem), -1.0);

        public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(CircleSurfaceItem), true);
        public static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(CircleSurfaceItem), true);

        public double BackgroundAngle { get => (double)GetValue(BackgroundAngleProperty); set => SetValue(BackgroundAngleProperty, value); }
        public double BackgroundAngleOffset { get => (double)GetValue(BackgroundAngleOffsetProperty); set => SetValue(BackgroundAngleOffsetProperty, value); }
        public Color BackgroundColor { get => (Color)GetValue(BackgroundColorProperty); set => SetValue(BackgroundColorProperty, value); }
        public int BackgroundLineWidth { get => (int)GetValue(BackgroundLineWidthProperty); set => SetValue(BackgroundLineWidthProperty, value); }
        public double BackgroundRadius { get => (double)GetValue(BackgroundRadiusProperty); set => SetValue(BackgroundRadiusProperty, value); }

        public double BarAngle { get => (double)GetValue(BarAngleProperty); set => SetValue(BarAngleProperty, value); }
        public double BarAngleOffset { get => (double)GetValue(BarAngleOffsetProperty); set => SetValue(BarAngleOffsetProperty, value); }
        public double BarAngleMaximum { get => (double)GetValue(BarAngleMaximumProperty); set => SetValue(BarAngleMaximumProperty, value); }
        public double BarAngleMinimum { get => (double)GetValue(BarAngleMinimumProperty); set => SetValue(BarAngleMinimumProperty, value); }
        public Color BarColor { get => (Color)GetValue(BarColorProperty); set => SetValue(BarColorProperty, value); }
        public int BarLineWidth { get => (int)GetValue(BarLineWidthProperty); set => SetValue(BarLineWidthProperty, value); }
        public double BarRadius { get => (double)GetValue(BarRadiusProperty); set => SetValue(BarRadiusProperty, value); }

        public bool IsVisible { get => (bool)GetValue(IsVisibleProperty); set => SetValue(IsVisibleProperty, value); }
        public bool IsEnabled { get => (bool)GetValue(IsEnabledProperty); set => SetValue(IsEnabledProperty, value); }
    }
}
