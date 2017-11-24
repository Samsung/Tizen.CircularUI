using System;
using System.Collections.Specialized;
using Xamarin.Forms.Internals;
using ElmSharp;
using Xamarin.Forms.Platform.Tizen;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(Xamarin.Forms.CircularUI.CircleListView), typeof(Xamarin.Forms.CircularUI.Renderer.CircleListViewRenderer))]

namespace Xamarin.Forms.CircularUI.Renderer
{
    public class CircleListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            if (Control == null)
            {
                var surface = this.GetSurface();
                SetNativeControl(new CircleListView(Platform.Tizen.Forms.Context.MainWindow, surface));
                Control.ItemSelected += OnListViewItemSelected;
                Control.ItemUnselected += OnListViewItemUnselected;
            }
            base.OnElementChanged(e);
        }
    }
}