using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms.CircularUI;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(TwoButtonPage), typeof(Xamarin.Forms.CircularUI.Renderer.TwoButtonPageRenderer))]


namespace Xamarin.Forms.CircularUI.Renderer
{
    public class TwoButtonPageRenderer : VisualElementRenderer<TwoButtonPage>
    {
        TwoButtonPageWidget _widget;

        public TwoButtonPageRenderer()
        {
            RegisterPropertyHandler(TwoButtonPage.BackgroundImageProperty, UpdateBackgroundImage);
            RegisterPropertyHandler(TwoButtonPage.FirstButtonProperty, UpdateFirstButton);
            RegisterPropertyHandler(TwoButtonPage.SecondButtonProperty, UpdateSecondButton);
            RegisterPropertyHandler(TwoButtonPage.TitleProperty, UpdateTitle);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TwoButtonPage> e)
        {
            if (_widget == null)
            {
                _widget = new TwoButtonPageWidget(Platform.Tizen.Forms.Context.MainWindow);
                _widget.LayoutUpdated += OnLayoutUpdated;
                SetNativeView(_widget);
            }

            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_widget != null)
                {
                    _widget.LayoutUpdated -= OnLayoutUpdated;
                }
            }
            base.Dispose(disposing);
        }

        protected override void UpdateBackgroundColor(bool initialize)
        {
            if (initialize && Element.BackgroundColor.IsDefault) return;

            if (Element.BackgroundColor.A == 0)
            {
                _widget.BackgroundColor = ElmSharp.Color.Transparent;
            }
            else
            {
                _widget.BackgroundColor = Element.BackgroundColor.ToNative();
            }
        }

        protected override void UpdateLayout()
        {
        }

        void OnLayoutUpdated(object sender, Platform.Tizen.Native.LayoutEventArgs e)
        {
            //DoLayout(e);
            //Element.Layout(_widget.Canvas.Geometry.ToDP());
            Element.Content.Layout(_widget.Canvas.Geometry.ToDP());
        }

        void UpdateBackgroundImage(bool initialize)
        {
            if (initialize && string.IsNullOrEmpty(Element.BackgroundImage))
                return;

            if (string.IsNullOrEmpty(Element.BackgroundImage))
                _widget.File = null;
            else
                _widget.File = ResourcePath.GetPath(Element.BackgroundImage);
        }

        void UpdateSecondButton(bool initialize)
        {
            Console.WriteLine($"Update Button2 init={initialize}, button={Element.SecondButton}");
            if (initialize && Element.SecondButton == null) return;
            if (Element.SecondButton != null)
            {
                var item = Element.SecondButton;
                _widget.ShowButton2(item.Text, item.Icon, () => item.Activate());
            }
            else
            {
                _widget.HideButton2();
            }
        }

        void UpdateFirstButton(bool initialize)
        {
            Console.WriteLine($"Update Button1 init={initialize}, button={Element.FirstButton}");
            if (initialize && Element.FirstButton == null) return;
            if (Element.FirstButton != null)
            {
                var item = Element.FirstButton;
                _widget.ShowButton1(item.Text, item.Icon, () => item.Activate());
            }
            else
            {
                _widget.HideButton1();
            }
        }
        void UpdateTitle()
        {
            _widget.Title = Element.Title;
        }
    }
}
